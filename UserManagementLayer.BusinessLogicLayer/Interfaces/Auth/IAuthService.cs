using UserManagementService.BusinessLogicLayer.Models.Commands;

namespace UserManagementService.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(UserRegistrationCommand registerRequest, CancellationToken cancellationToken);
        Task<string> LoginAsync(UserLoginCommand loginRequest, CancellationToken cancellationToken);
    }
}
