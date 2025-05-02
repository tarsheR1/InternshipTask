using FluentValidation.Results;
using UserManagementService.BusinessLogicLayer.Models.DTO.ValidationError;

namespace UserManagementService.BusinessLogicLayer.Exceptions.Core
{

    public class ValidationException : BusinessLogicException
    {
        public IEnumerable<ValidationError> Errors { get; }

        public ValidationException(string errorCode, string message)
            : base(errorCode, message)
        {
            Errors = new[] { new ValidationError(errorCode, message) };
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base("validation_error", "Ошибка валидации")
        {
            Errors = failures.Select(f => new ValidationError(
                f.ErrorCode ?? "validation_error",
                f.ErrorMessage,
                f.PropertyName
            ));
        }

        public ValidationException(IEnumerable<ValidationError> errors)
            : base("validation_error", "Ошибка валидации")
        {
            Errors = errors;
        }
    }
}
