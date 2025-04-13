using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagementService.BusinessLogicLayer.Models.Settings;
using UserManagementService.BusinessLogicLayer.Extensions;
using UserManagementService.DataAccessLayer.Extensions;
using UserManagementService.PresentationLayer.Extensions;
using MapsterMapper;
using AutoMapper;
using UserManagementService.BusinessLogicLayer.Services.ExternalServices.Mapping;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
}); 

builder.Services.AddDataAccessLayer(builder.Configuration);

builder.Services.AddBusinessLogicLayer(builder.Configuration);

MapsterConfig.Configure();

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
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
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

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddScoped<MapsterMapper.IMapper, MapsterMapper.Mapper>(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");
//app.UseCors(builder =>
//builder.AllowAnyOrigin()
//    .AllowAnyHeader()
//    .AllowAnyMethod());


app.UseGlobalErrorHandling();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
