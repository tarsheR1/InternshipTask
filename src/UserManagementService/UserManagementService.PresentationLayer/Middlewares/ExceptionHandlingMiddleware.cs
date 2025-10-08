using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace UserManagementService.PresentationLayer.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception has occurred");

            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Message = "An internal server error has occurred",
                TraceId = context.TraceIdentifier
            };

            switch (exception)
            {
                case DbUpdateException dbEx:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    errorResponse.Message = "Database error occurred";
                    errorResponse.Details = GetDbErrorMessage(dbEx);
                    break;

                case UnauthorizedAccessException:
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    errorResponse.Message = "Access denied";
                    break;

                case KeyNotFoundException:
                    response.StatusCode = StatusCodes.Status404NotFound;
                    errorResponse.Message = "Resource not found";
                    break;

                case ValidationException vex:
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    errorResponse.Message = "Validation failed";
                    errorResponse.Errors = ConvertValidationErrors(vex.Errors);
                    break;

                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    if (context.RequestServices.GetService<IWebHostEnvironment>().IsDevelopment())
                    {
                        errorResponse.Details = exception.ToString();
                    }
                    break;
            }

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }

        private string GetDbErrorMessage(DbUpdateException dbEx)
        {
            return dbEx.InnerException?.Message ?? dbEx.Message;
        }

            private Dictionary<string, string[]> ConvertValidationErrors(IEnumerable<ValidationFailure> errors)
            {
                return errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );
            }
        }


        public class ErrorResponse
        {
            public string Message { get; set; }
            public string TraceId { get; set; }
            public string Details { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }
}
