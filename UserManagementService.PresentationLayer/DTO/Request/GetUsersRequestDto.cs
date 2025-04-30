namespace UserManagementService.PresentationLayer.DTO.Request
{
    public class GetUsersRequestDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; }
        public bool? IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string SortField { get; set; } = "CreatedAt";
        public bool IsDescending { get; set; } = true;
    }
}
