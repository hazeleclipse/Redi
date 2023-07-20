using Redi.Application.Extensions;
using Redi.Infrastructure.Extensions;
using Redi.MinimalApi.Containers;
using Redi.MinimalApi.Stakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure();

var app = builder.Build();

// Stakers
var stakers = app.MapGroup("/api/stakers");
stakers.MapGet("/", StakerHandler.GetAllStakers);
stakers.MapPost("/", StakerHandler.CreateStaker);
stakers.MapGet("/{id}", StakerHandler.GetStakerById);
stakers.MapDelete("/{id}", StakerHandler.DeleteStaker);

// Containers
var containers = app.MapGroup("/api/containers");
containers.MapGet("/", ContainerHandler.GetAllContainers);
containers.MapPost("/", ContainerHandler.CreateContainer);
containers.MapGet("/{id}", ContainerHandler.GetContainerById);

app.Run();