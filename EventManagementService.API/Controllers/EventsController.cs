using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventManagementService.Application.Queries;
using EventManagementService.Application.UseCases.Сommands.Events;

namespace EventManagementService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventEntity>> GetEventById(Guid id, CancellationToken cancellation)
        {
            var query = new GetEventByIdQuery { Id = id };
            var eventEntity = await _mediator.Send(query, cancellation);
            return Ok(eventEntity);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<(List<EventEntity> Events, 
            int TotalCount)>> GetPagedEvents(int pageNumber, 
            int pageSize, 
            CancellationToken cancellation)
        {
            var query = new GetPagedEventsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command, CancellationToken cancellation)
        {
            var eventId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEventById), new { id = eventId }, eventId);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEvent(
            Guid id, 
            [FromBody] UpdateEventCommand command, 
            CancellationToken cancellation)
        {
            if (id != command.Id)
            {
                return BadRequest("ID в маршруте и теле запроса не совпадают.");
            }

            await _mediator.Send(command, cancellation);
            return Ok(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellation)
        {
            var command = new DeleteEventCommand { Id = id };
            await _mediator.Send(command);
            return Ok(); 
        }
    }
}
