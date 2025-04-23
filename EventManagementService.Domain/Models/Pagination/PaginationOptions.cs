namespace EventManagementService.Domain.Models.Pagination
{
    public class PaginationOptions
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
