using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Entities;
using Ticketing.Domain.Enums;
using static Ticketing.Domain.Enums.Enums;
using Ticketing.Application.DTOs;
namespace Ticketing.Application.Interfaces
{
    public interface ITicketRepository
    {
        void Add(Ticket ticket);
        List<Ticket> GetAll();
        List<Ticket> GetByCreator(Guid userId);
        Ticket? GetById(Guid id);
        void Update(Ticket ticket);
        void Delete(Ticket ticket);
        Dictionary<TicketStatus, int> GetTicketCountsByStatus();
    }
}
