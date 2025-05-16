using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using UserManagementService.BusinessLogicLayer.Interfaces.Infrastructure;
using UserManagementService.BusinessLogicLayer.Services.ExternalServices;

public static class UrlHelperExtensions
{
    public static IServiceCollection ConfigureUrlHelpers(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

        services.AddScoped<IUrlHelper>(x =>
        {
            var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext
                ?? throw new InvalidOperationException("ActionContext is not available");
            return new UrlHelper(actionContext);
        });

        services.AddSingleton<IHangfireUrlHelper, HangfireUrlHelper>();

        return services;
    }
}