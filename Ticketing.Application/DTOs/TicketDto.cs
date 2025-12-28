using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ticketing.Domain.Enums.Enums;

namespace Ticketing.Application.DTOs
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
    }
}
