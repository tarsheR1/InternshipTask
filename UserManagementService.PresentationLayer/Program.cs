using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.DataAccessLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddRepositories();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);
    
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApproveEvent", policy =>
        policy.RequireClaim("permission", "ApproveEvent"));

    options.AddPolicy("ModerateEvents", policy =>
        policy.RequireClaim("permission", "ModerateEvents"));

    options.AddPolicy("BuyEventTicket", policy =>
        policy.RequireClaim("permission", "BuyEventTicket"));

    options.AddPolicy("ProposeEvent", policy =>
        policy.RequireClaim("permission", "ProposeEvent"));

    options.AddPolicy("ModerateUsers", policy =>
        policy.RequireClaim("permission", "ModerateUsers"));
});

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
