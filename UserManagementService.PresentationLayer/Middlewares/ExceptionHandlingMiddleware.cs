using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Exceptions.Auth;
using UserManagementService.BusinessLogicLayer.Exceptions.Core;
using UserManagementService.BusinessLogicLayer.Exceptions.Token;
using UserManagementService.BusinessLogicLayer.Exceptions.Users;
using UserManagementService.BusinessLogicLayer.Exceptions.Jwt;

namespace UserManagementService.PresentationLayer.Middlewares
{
    public class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public BusinessExceptionMiddleware(RequestDelegate next, ILogger<BusinessExceptionMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessLogicException ex)
            {
                await HandleBusinessException(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericException(context, ex);
            }
        }

        private async Task HandleBusinessException(HttpContext context, BusinessLogicException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                NotFoundException => 404,
                InvalidCredentialsException or InvalidTokenException => 401,
                ConflictException or AlreadyExistsException => 409,
                _ => 400
            };

            var response = new
            {
                error = ex.ErrorCode,
                message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleGenericException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new
            {
                error = "internal_error",
                message = "Произошла необработанная ошибка"
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
