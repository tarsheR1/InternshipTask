using EventManagementService.DataAccess.Persistence;
using EventManagementService.Domain.Entities;
using EventManagementService.Domain.Interfaces.Repositories;
using EventManagementService.Domain.Interfaces.Specification;
using EventManagementService.Domain.Pagination;
using EventManagementService.Domain.Specification.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventManagementService.Infrastructure.DataAccess.Repositories
{
    public class EventRepository : BaseRepository<EventEntity, Guid>, IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {
        }
    }    
}
