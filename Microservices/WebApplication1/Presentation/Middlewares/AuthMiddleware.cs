using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace EventManagementService.Presentation.Middlewares
{
    //TODO
    public class AuthMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next) { }
        //private readonly IUserService _userService;

        //public AuthMiddleware(IUserService userService)
        //{
        //    _userService = userService;
        //}

        //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    if (!context.Request.Headers.TryGetValue("Authorization", out StringValues authHeader))
        //    {
        //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //        await context.Response.WriteAsync("Authorization header is missing.");
        //        return;
        //    }

        //    var token = authHeader.ToString().Replace("Bearer ", "");

        //    var isValid = await _userService.ValidateTokenAsync(token);
        //    if (!isValid)
        //    {
        //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //        await context.Response.WriteAsync("Invalid token.");
        //        return;
        //    }

        //    //var claims = await _userService.GetUserClaimsAsync(token);
        //    //var identity = new ClaimsIdentity(claims, "Bearer");
        //    context.User = new ClaimsPrincipal(identity);

        //    await next(context);
        //}
    }
}
