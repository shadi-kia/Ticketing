using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Enums;
using static Ticketing.Domain.Enums.Enums;

namespace Ticketing.Application.DTOs
{
    public class UpdateTicketDto
    {
        public TicketStatus Status { get; set; }
        public Guid? AssignedToUserId { get; set; }
    }
}
