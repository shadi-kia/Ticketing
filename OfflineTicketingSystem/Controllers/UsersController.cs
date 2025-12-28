using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketing.Domain.Entities;
using Ticketing.Infrastructure.Data;
using Ticketing.Application.DTOs;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly TicketingDbContext _context;

    public UsersController(TicketingDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _context.Users
            .Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.Role,
                u.PasswordHash
            })
            .ToList();

        return Ok(users);
    }

    [HttpPost("register", Name = "CreateUser")]
    public IActionResult Register([FromBody] CreateUserDto request)
    {
        var exists = _context.Users.Any(u => u.Email == request.Email);
        if (exists)
            return BadRequest("User already exists");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName=request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            Role = request.Role
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new
        {
            user.Id,
            user.Email,
            user.Role,
            user.FullName
        });
    }
}


