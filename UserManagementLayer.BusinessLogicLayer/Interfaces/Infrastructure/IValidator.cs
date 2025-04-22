using System.ComponentModel.DataAnnotations;

namespace UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure
{
    public interface IValidator<T>
    {
        ValidationResult Validate(T instance);
    }
}
