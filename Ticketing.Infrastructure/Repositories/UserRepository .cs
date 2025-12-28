using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Data;

namespace Ticketing.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TicketingDbContext _context;

    public UserRepository(TicketingDbContext context)
    {
        _context = context;
    }

    public User? GetById(Guid id)
        => _context.Users.FirstOrDefault(u => u.Id == id);

    public User? GetByEmail(string email)
        => _context.Users.FirstOrDefault(u => u.Email == email);

    public List<User> GetAll()
        => _context.Users.ToList();
}
