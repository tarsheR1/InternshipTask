using System.Reflection;
using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.DataAccessLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddUsersDbContext(builder.Configuration);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddRepositories();
builder.Services.AddServices();


builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

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
