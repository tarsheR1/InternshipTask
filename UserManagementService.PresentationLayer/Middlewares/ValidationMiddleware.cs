using System.Net;
using System.Text.Json;

namespace UserManagementService.PresentationLayer.Middlewares
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationMiddleware> _logger;

        public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
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
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validation error occurred");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    Title = "Validation Error",
                    Status = context.Response.StatusCode,
                    Errors = ex.Errors.Select(e => new
                    {
                        Property = e.PropertyName,
                        Message = e.ErrorMessage,
                        Code = e.ErrorCode
                    })
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
