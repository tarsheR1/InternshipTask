namespace UserManagementService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(string email, string password, string firstName, string lastName, string middleName, string phone);
        Task<string> LoginAsync(string email, string password);
    }
}
