using FluentAssertions;
using Moq;
using Ticketing.Application.DTOs;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;
using Xunit;
using static Ticketing.Domain.Enums.Enums;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepository> _ticketRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly TicketService _service;

    public TicketServiceTests()
    {
        _ticketRepoMock = new Mock<ITicketRepository>();
        _userRepoMock = new Mock<IUserRepository>();

        _service = new TicketService(
            _ticketRepoMock.Object,
            _userRepoMock.Object
        );
    }

    [Fact]
    public void CreateTicket_Should_Create_Ticket_When_Data_Is_Valid()
    {
        // Arrange
        var employeeId = Guid.NewGuid();

        _userRepoMock
            .Setup(r => r.GetById(employeeId))
            .Returns(new User
            {
                Id = employeeId,
                Role = UserRole.Employee
            });

        var dto = new CreateTicketDto
        {
            Title = "Test Ticket",
            Description = "Test Desc",
            Priority = TicketPriority.Medium
        };

        // Act
        var result = _service.CreateTicket(employeeId, dto);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Ticket");
        result.Status.Should().Be(TicketStatus.Open);

        _ticketRepoMock.Verify(
            r => r.Add(It.IsAny<Ticket>()),
            Times.Once
        );
    }

    [Fact]
    public void CreateTicket_Should_Throw_When_User_Not_Found()
    {
        var userId = Guid.NewGuid();

        _userRepoMock.Setup(r => r.GetById(userId))
            .Returns((User?)null);

        var dto = new CreateTicketDto
        {
            Title = "X",
            Description = "Y",
            Priority = TicketPriority.High
        };

        Action act = () => _service.CreateTicket(userId, dto);

        act.Should().Throw<Exception>()
           .WithMessage("User not found");
    }
}
