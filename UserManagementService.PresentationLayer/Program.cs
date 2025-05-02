using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.PresentationLayer.Extensions.Application;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var app = builder.Build();

app.ConfigurePipeline();

app.Run();
