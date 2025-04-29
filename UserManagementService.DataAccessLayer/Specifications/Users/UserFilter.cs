namespace UserManagementService.DataAccessLayer.Specifications.Users
{
    public class UserFilter
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
