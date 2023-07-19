using Redi.Application.Extensions;
using Redi.Infrastructure.Extensions;
using Redi.MinimalApi.Stakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Stakers
var stakers = app.MapGroup("/api/stakers");
stakers.MapGet("/", StakerHandler.GetAllStakers);
stakers.MapPost("/", StakerHandler.CreateStaker);
stakers.MapGet("/{id}", StakerHandler.GetStakerById);
stakers.MapPut("/{id}", StakerHandler.UpdateStaker);
stakers.MapDelete("/{id}", StakerHandler.DeleteStaker);

// Containers
var containers = app.MapGroup("/api/containers");

app.Run();