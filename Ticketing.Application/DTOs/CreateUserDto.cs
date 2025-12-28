using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Domain.Enums;

namespace Ticketing.Application.DTOs
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Enums.UserRole Role { get; set; }
    }
}
