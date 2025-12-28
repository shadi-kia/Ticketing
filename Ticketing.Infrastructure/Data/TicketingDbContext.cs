using System;
using BCrypt.Net;
using System.Data;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ticketing.Domain.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using static Ticketing.Domain.Enums.Enums;

namespace Ticketing.Infrastructure.Data
{
    public class TicketingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        public TicketingDbContext(DbContextOptions<TicketingDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminId = new Guid("11111111-1111-1111-1111-111111111111");
            var employeeId = new Guid("22222222-2222-2222-2222-222222222222");
            var ticketId = new Guid("33333333-3333-3333-3333-333333333333");

            var seedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    FullName = "Admin User",
                    Email = "admin@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("emp123"),
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = employeeId,
                    FullName = "Employee User",
                    Email = "employee@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("emp123"),
                    Role = UserRole.Employee
                }
            );

            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = ticketId,
                    Title = "Initial Ticket",
                    Description = "Seeded ticket",
                    Status = TicketStatus.Open,
                    Priority = TicketPriority.Medium,
                    CreatedByUserId = employeeId,
                    CreatedAt = seedDate,
                    UpdatedAt = seedDate
                }
            );

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.CreatedByUser)
                .WithMany(u => u.CreatedTickets)
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.AssignedToUser)
                .WithMany(u => u.AssignedTickets)
                .HasForeignKey(t => t.AssignedToUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
