using Redi.Application.Extensions;
using Redi.Infrastructure.Extensions;
using Redi.MinimalApi.Containers;
using Redi.MinimalApi.Nodes;
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

stakers.MapDelete("/{id}", StakerHandler.DeleteStaker);
stakers.MapGet("/{id}", StakerHandler.GetStakerById);

// Containers
var containers = app.MapGroup("/api/containers");
containers.MapGet("/", ContainerHandler.GetAllContainers);
containers.MapPost("/", ContainerHandler.CreateContainer);

containers.MapDelete("/{id}", ContainerHandler.DeleteContainer);
containers.MapGet("/{id}", ContainerHandler.GetContainerById);
containers.MapPut("/{id}", ContainerHandler.UpdateContainer);

containers.MapDelete("/{id}/containers/{childId}", ContainerHandler.RemoveChildContainer);
containers.MapPost("/{id}/containers/{childId}", ContainerHandler.AddChildContainer);
containers.MapPut("/{id}/containers/{childId}", ContainerHandler.EditChildContainer);

containers.MapDelete("/{id}/stakers/{childId}", ContainerHandler.RemoveChildStaker);
containers.MapPost("/{id}/stakers/{childId}", ContainerHandler.AddChildStaker);
containers.MapPut("/{id}/stakers/{childId}", ContainerHandler.EditChildStaker);

// Nodes
var nodes = app.MapGroup("/api/nodes");
nodes.MapGet("/", NodeHanlder.GetAllNodes);
nodes.MapPost("/", NodeHanlder.CreateNode);

app.Run();