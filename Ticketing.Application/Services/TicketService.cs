using Ticketing.Application.DTOs;
using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;
using static Ticketing.Domain.Enums.Enums;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUserRepository _userRepository;


    public TicketService(ITicketRepository ticketRepository,IUserRepository userRepository)
    {
        _ticketRepository = ticketRepository;
        _userRepository = userRepository;
    }

    public Ticket CreateTicket(Guid userId, CreateTicketDto request)
    {

        if (request.AssignedToUserId != null)
        {
            var user = _userRepository.GetById(request.AssignedToUserId.Value);

            if (user == null || user.Role != UserRole.Admin)
                throw new Exception("Assigned user must be an Admin");
        }

        var ticket = new Ticket
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = TicketStatus.Open,
            CreatedByUserId = userId,
            AssignedToUserId= request.AssignedToUserId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _ticketRepository.Add(ticket);
        return ticket;
    }

    public List<Ticket> GetMyTickets(Guid userId)
        => _ticketRepository.GetByCreator(userId);

    public List<Ticket> GetAllTickets()
        => _ticketRepository.GetAll();

    public Ticket UpdateTicket(Guid ticketId, UpdateTicketDto request)
    {
        var ticket = _ticketRepository.GetById(ticketId)
            ?? throw new Exception("Ticket not found");

        ticket.Status = request.Status;
        ticket.AssignedToUserId = request.AssignedToUserId;
        ticket.UpdatedAt = DateTime.Now;

        _ticketRepository.Update(ticket);
        return ticket;
    }

    public object GetTicketCountsByStatus()
        => _ticketRepository.GetTicketCountsByStatus();

    public Ticket GetById(Guid ticketId, Guid userId, string role)
    {
        var ticket = _ticketRepository.GetById(ticketId)
            ?? throw new Exception("Ticket not found");

        if (role == "Admin" || ticket.CreatedByUserId == userId)
            return ticket;

        throw new UnauthorizedAccessException();
    }

    public void Delete(Guid ticketId)
    {
        var ticket = _ticketRepository.GetById(ticketId)
            ?? throw new Exception("Ticket not found");

        _ticketRepository.Delete(ticket);
    }
}
