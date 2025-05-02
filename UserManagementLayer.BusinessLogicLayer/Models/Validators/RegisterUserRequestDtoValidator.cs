using FluentValidation;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;

namespace UserManagementService.BusinessLogicLayer.Models.Validators
{
    public class RegisterUserRequestDtoValidator : AbstractValidator<RegisterUserRequestDto>
    {
        public RegisterUserRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email обязателен")
                .EmailAddress().WithMessage("Некорректный формат email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен")
                .MinimumLength(8).WithMessage("Пароль должен содержать минимум 8 символов")
                .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву")
                .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву")
                .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(50).WithMessage("Имя не должно превышать 50 символов");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия обязательна")
                .MaximumLength(50).WithMessage("Фамилия не должна превышать 50 символов");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Некорректный формат телефона")
                .When(x => !string.IsNullOrEmpty(x.Phone));
        }
    }
}
