namespace UserManagementService.BusinessLogicLayer.Models.Pagination
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public PagedResponse(
            IEnumerable<T> data,
            int pageNumber,
            int pageSize,
            int totalCount)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
