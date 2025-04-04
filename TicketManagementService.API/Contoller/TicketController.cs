using Microsoft.AspNetCore.Mvc;
using MediatR;
using TicketManagementService.Application.Commands;
using TicketManagementService.Application.Queries;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("inventories/{eventId:guid}")]
    public async Task<ActionResult<IReadOnlyList<TicketInventory>>> GetInventories(Guid eventId)
    {
        var query = new GetTicketInventoriesForEventQuery(eventId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("reserve")]
    public async Task<IActionResult> ReserveTickets([FromBody] ReserveTicketsCommand command)
    {
        try
        {
            await _mediator.Send(command);
            return Ok();
        }
        catch (TicketsSoldOutException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpPost("release")]
    public async Task<IActionResult> ReleaseTickets([FromBody] ReleaseTicketsCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPut("inventory")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateInventory([FromBody] UpdateTicketInventoryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}