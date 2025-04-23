using EventManagementService.Domain.Models;
using MediatR;

namespace EventManagementService.Application.Queries
{
    public class GetPagedEventsQuery : IRequest<(List<EventEntity> Events, int TotalCount)>
    {
        public int PageNumber { get; set; } 
        public int PageSize { get; set; } 
    }
}
