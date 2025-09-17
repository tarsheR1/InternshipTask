using EventManagementService.Domain.Pagination;
using System.Linq;

namespace EventManagementService.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationOptions pagination)
        {
            return query
                .Skip(pagination.Skip)
                .Take(pagination.Take);
        }
    }
}
