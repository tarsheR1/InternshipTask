using FluentValidation;
using UserManagementService.PresentationLayer.DTO.Request;

namespace UserManagementService.PresentationLayer.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email!)
                    .EmailAddress().WithMessage("Invalid email format")
                    .MaximumLength(254).WithMessage("Email must not exceed 254 characters")
                    .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email format");
            });

            When(x => !string.IsNullOrEmpty(x.FirstName), () =>
            {
                RuleFor(x => x.FirstName!)
                    .MaximumLength(50).WithMessage("First name must not exceed 50 characters")
                    .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s\-]+$").WithMessage("First name can only contain letters, spaces, and hyphens");
            });

            When(x => !string.IsNullOrEmpty(x.LastName), () =>
            {
                RuleFor(x => x.LastName!)
                    .MaximumLength(50).WithMessage("Last name must not exceed 50 characters")
                    .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s\-]+$").WithMessage("Last name can only contain letters, spaces, and hyphens");
            });

            When(x => !string.IsNullOrEmpty(x.MiddleName), () =>
            {
                RuleFor(x => x.MiddleName!)
                    .MaximumLength(50).WithMessage("Middle name must not exceed 50 characters")
                    .Matches(@"^[a-zA-Zа-яА-ЯёЁ\s\-]+$").WithMessage("Middle name can only contain letters, spaces, and hyphens");
            });

            When(x => !string.IsNullOrEmpty(x.Phone), () =>
            {
                RuleFor(x => x.Phone!)
                    .MaximumLength(20).WithMessage("Phone must not exceed 20 characters")
                    .Matches(@"^[\+\-\s\d\(\)]+$").WithMessage("Invalid phone format");
            });

            RuleFor(x => x)
                .Must(x => !string.IsNullOrEmpty(x.Email) ||
                          !string.IsNullOrEmpty(x.FirstName) ||
                          !string.IsNullOrEmpty(x.LastName) ||
                          !string.IsNullOrEmpty(x.MiddleName) ||
                          !string.IsNullOrEmpty(x.Phone))
                .WithMessage("At least one field must be provided");
        }
    }
}