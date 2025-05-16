namespace UserManagementService.DataAccessLayer.Entities.Base
{
    public class BaseEntity <TId>
    {
        public TId Id { get; set; }
    }
}
