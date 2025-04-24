using UserManagementService.PresentationLayer.Middlewares;

namespace UserManagementService.PresentationLayer.Extensions
{
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
