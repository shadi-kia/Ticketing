using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Application.DTOs;
using Ticketing.Domain.Entities;

namespace Ticketing.Application.Interfaces
{
    public interface ITicketService
    {
        Ticket CreateTicket(Guid userId, CreateTicketDto request);
        List<Ticket> GetMyTickets(Guid userId);
        List<Ticket> GetAllTickets();
        Ticket UpdateTicket(Guid ticketId, UpdateTicketDto request);
        object GetTicketCountsByStatus();
        Ticket GetById(Guid ticketId, Guid userId, string role);
        void Delete(Guid ticketId);

    }
}
