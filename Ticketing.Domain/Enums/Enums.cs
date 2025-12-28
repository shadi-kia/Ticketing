using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Domain.Enums
{
    public class Enums
    {
        public enum UserRole
        {
            Employee=1,
            Admin=2
        }

        public enum TicketStatus
        {
            Open = 1,
            InProgress = 2,
            Closed=3
        }


        public enum TicketPriority
        {
            Low = 1,
            Medium = 2,
            High = 3
        }


    }
}
