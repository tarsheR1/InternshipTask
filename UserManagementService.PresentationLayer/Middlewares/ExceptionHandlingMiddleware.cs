using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Core;
using UserManagementService.BusinessLogicLayer.Exceptions.Token;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Jwt;

namespace UserManagementService.PresentationLayer.Middlewares
{

    // TODO:  I will add the logger when there is an ELC stack.
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(ex);

            var errorResponse = new
            {
                ErrorCode = (ex as BusinessLogicException)?.ErrorCode ?? "internal_error",
                Message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(errorResponse);            
        }

        private static int GetStatusCode(Exception ex)
        {
            return ex switch
            {
                // Auth
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                InvalidRefreshTokenException => StatusCodes.Status401Unauthorized,
                EmailAlreadyExistsException => StatusCodes.Status409Conflict,

                // JWT
                JwtConfigurationException => StatusCodes.Status500InternalServerError,

                // Token
                InvalidTokenException => StatusCodes.Status401Unauthorized,
                TokenAlreadyRevokedException => StatusCodes.Status400BadRequest,
                TokenGenerationException => StatusCodes.Status500InternalServerError,
                TokenNotFoundException => StatusCodes.Status404NotFound,
                TokenRevocationException => StatusCodes.Status500InternalServerError,
                TokenValidationException => StatusCodes.Status401Unauthorized,

                // Users
                AlreadyExistsException => StatusCodes.Status409Conflict,
                ConflictException => StatusCodes.Status409Conflict,
                NotFoundException => StatusCodes.Status404NotFound,
                UserNotFoundException => StatusCodes.Status404NotFound,

                BusinessLogicException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
    }
}
