var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.AddAu
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

var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

// Регистрация MongoDB клиента
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(mongoDbSettings.ConnectionString));

// Регистрация базы данных
builder.Services.AddScoped<IMongoDatabase>(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDbSettings.DatabaseName));

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
