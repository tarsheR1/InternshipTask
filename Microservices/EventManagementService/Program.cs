using MediatR;
using EventManagementService.Application.Handlers;
using EventManagementService.Application.Ñommands;
using EventManagementService.Domain.Interfaces;
using EventManagementService.Infrastracture.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped<IEventRepository, EventRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
