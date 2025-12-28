using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Ticketing.Application.DTOs;
using Ticketing.Application.Interfaces;

namespace Ticketing.API.Controllers;

[ApiController]
[Route("tickets")]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("CreateTicket")]
    [Authorize(Roles = "Employee")]
    public IActionResult CreateTicket([FromBody] CreateTicketDto request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var ticket = _ticketService.CreateTicket(userId, request);
        return Ok(ticket);
    }

    [HttpGet("MyTickets")]
    [Authorize(Roles = "Employee")]
    public IActionResult MyTickets()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        return Ok(_ticketService.GetMyTickets(userId));
    }

    [HttpGet("GetAllTickets")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllTickets()
    {
        return Ok(_ticketService.GetAllTickets());
    }

    [HttpPut("{id}", Name = "UpdateTicket")]
    [Authorize(Roles = "Admin")]
    public IActionResult UpdateTicket(Guid id,[FromBody] UpdateTicketDto request)
    {
        var ticket = _ticketService.UpdateTicket(id, request);
        return Ok(ticket);
    }

    [HttpGet("GetTicketCountsByStatus")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetTicketCountsByStatus()
    {
        return Ok(_ticketService.GetTicketCountsByStatus());
    }

    [HttpGet("{id}", Name = "GetById")]
    public IActionResult GetTicketById(Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var ticket = _ticketService.GetById(id, userId, role);
        return Ok(ticket);
    }

    [HttpDelete("{id}",Name = "DeleteTicket")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteTicket(Guid id)
    {
        _ticketService.Delete(id);
        return NoContent();
    }
}
