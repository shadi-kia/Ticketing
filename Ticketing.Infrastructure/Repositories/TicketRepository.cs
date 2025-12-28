using Ticketing.Application.Interfaces;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;
using Ticketing.Infrastructure.Data;
using static Ticketing.Domain.Enums.Enums;

namespace Ticketing.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly TicketingDbContext _context;

    public TicketRepository(TicketingDbContext context)
    {
        _context = context;
    }

    public void Add(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        _context.SaveChanges();
    }

    public List<Ticket> GetAll()
        => _context.Tickets.ToList();

    public List<Ticket> GetByCreator(Guid userId)
        => _context.Tickets
            .Where(t => t.CreatedByUserId == userId)
            .ToList();

    public Ticket? GetById(Guid id)
        => _context.Tickets.Find(id);

    public void Update(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        _context.SaveChanges();
    }

    public void Delete(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
    }

    public Dictionary<TicketStatus, int> GetTicketCountsByStatus()
        => _context.Tickets
            .GroupBy(t => t.Status)
            .ToDictionary(g => g.Key, g => g.Count());
}
