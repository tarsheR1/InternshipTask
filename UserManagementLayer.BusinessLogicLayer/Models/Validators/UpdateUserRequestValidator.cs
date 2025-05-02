using FluentValidation;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;

namespace UserManagementService.BusinessLogicLayer.Models.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Некорректный формат email")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.FirstName)
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов")
                .When(x => !string.IsNullOrEmpty(x.FirstName));

            RuleFor(x => x.LastName)
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов")
                .When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Некорректный формат телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));
        }
    }
}
