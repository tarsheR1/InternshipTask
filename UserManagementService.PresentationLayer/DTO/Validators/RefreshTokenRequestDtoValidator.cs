using FluentValidation;
using UserManagementService.PresentationLayer.DTO.Request;

namespace UserManagementService.PresentationLayer.DTO.Validators
{
    public class RefreshTokenRequestDtoValidator : AbstractValidator<RefreshTokenRequestDto>
    {
        public RefreshTokenRequestDtoValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Refresh token обязателен");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID обязателен");
        }
    }
}
