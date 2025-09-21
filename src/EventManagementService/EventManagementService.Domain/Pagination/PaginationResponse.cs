namespace EventManagementService.Domain.Pagination
{
    public record PaginationResponse<T>(
        List<T> Items,
        int TotalCount,
        int PageNumber,
        int PageSize
    );
}
