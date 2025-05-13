using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.BusinessLogicLayer.Mapping;
using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.BusinessLogicLayer.Validators.LoginRequestDtoValidator;
using UserManagementService.DataAccessLayer.Extensions;
using UserManagementService.PresentationLayer.Extensions.Configuration;
using UserManagementService.PresentationLayer.Extensions.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

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
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>()
    .Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseGlobalErrorHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
