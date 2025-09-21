namespace EventManagementService.Domain.Pagination
{
    public class PaginationOptions(
        int PageNumber = 1,
        int PageSize = 10)
    {
        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
    }
}
