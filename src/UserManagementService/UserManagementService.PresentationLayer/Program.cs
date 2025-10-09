using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.DataAccessLayer.Extensions;
using UserManagementService.PresentationLayer.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddFluentValidationAutoValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
}); 
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSwaggerGen();

builder.Services.AddUsersDbContext(builder.Configuration);

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddAutoMapper(
    typeof(Program).Assembly,
    typeof(UserManagementService.BusinessLogicLayer.MappingProfiles.UserProfile).Assembly);

builder.Services.AddRepositories();
builder.Services.AddServices();

var app = builder.Build();

app.UseCustomMiddlewares();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
