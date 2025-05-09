using FluentValidation;
using UserManagementService.BusinessLogicLayer.Models.DTO.Request;

namespace UserManagementService.BusinessLogicLayer.Validators.RefreshTokenRequestDtoValidator
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
