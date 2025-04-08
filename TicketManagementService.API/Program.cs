using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TicketManagementService.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BuyTicket", policy =>
        policy.RequireClaim("permission", "BuyTicket"));

    options.AddPolicy("ManageTickets", policy =>
        policy.RequireClaim("permission", "ModerateEvents"));
});


var app = builder.Build();

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(mongoDbSettings.ConnectionString));

builder.Services.AddScoped<IMongoDatabase>(serviceProvider =>
{
    var client = serviceProvider.GetRequiredService<IMongoClient>();
    var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return client.GetDatabase(settings.DatabaseName);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
