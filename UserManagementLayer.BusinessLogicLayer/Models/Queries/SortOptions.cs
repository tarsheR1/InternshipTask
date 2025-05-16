namespace UserManagementService.BusinessLogicLayer.Models.Queries
{
    public class SortOptions
    {
        public string Field { get; set; } = "Email";
        public bool IsDescending { get; set; } = true;
    }
}
