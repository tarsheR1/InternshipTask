namespace UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure
{
    public interface IHangfireUrlHelper
    {
        string GenerateAbsoluteUrl(string action, string controller, object values);
    }
}
