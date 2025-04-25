namespace EventManagementService.Application.DTO.Pagination
{
    public record PaginationResponse<T>(
        List<T> Items,
        int TotalCount,
        int PageNumber,
        int PageSize
    );
}
