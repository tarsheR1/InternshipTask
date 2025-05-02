var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var app = builder.Build();
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
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
