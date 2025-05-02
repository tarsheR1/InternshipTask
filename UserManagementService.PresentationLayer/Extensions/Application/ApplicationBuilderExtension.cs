using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.BusinessLogicLayer.Models.DTO.Validators;
using UserManagementService.BusinessLogicLayer.Models.Settings.Mapping;
using UserManagementService.DataAccessLayer.Extensions;
using UserManagementService.PresentationLayer.Extensions.Configuration;

namespace UserManagementService.PresentationLayer.Extensions.Application
{
    public static class WebApplicationBuilderExtension
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services
                .ConfigureCors()
                .AddDataAccessLayer(builder.Configuration)
                .AddBusinessLogicLayer(builder.Configuration)
                .AddJwtAuthentication(builder.Configuration)
                .AddCustomAuthorizationPolicies()
                .AddSwaggerWithJwtAuth()
                .AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>(ServiceLifetime.Scoped)
                .AddAutoMapper(typeof(UserProfile))
                .Configure<ApiBehaviorOptions>(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
        }
    }
}
