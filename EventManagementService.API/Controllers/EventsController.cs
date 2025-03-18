using EventManagementService.Application.Queries;
using EventManagementService.Application.Сommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EventManagementService.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementService.Presentation.Controllers
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
        public async Task<ActionResult<EventEntity>> GetEventById(Guid id)
        {
            var query = new GetEventByIdQuery { Id = id };
            var eventEntity = await _mediator.Send(query);
            return Ok(eventEntity);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<(List<EventEntity> Events, int TotalCount)>> GetPagedEvents(int pageNumber, int pageSize)
        {
            var query = new GetPagedEventsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command)
        {
            var eventId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetEventById), new { id = eventId }, eventId);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID в маршруте и теле запроса не совпадают.");
            }

            await _mediator.Send(command);
            return Ok(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var command = new DeleteEventCommand { Id = id };
            await _mediator.Send(command);
            return Ok(); 
        }
    }
}
