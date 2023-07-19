using MediatR;
using Redi.Application.Extensions;
using Redi.Application.Stakers.Commands.Register;
using Redi.Application.Stakers.Queries.GetById;
using Redi.Infrastructure.Extensions;
using Redi.MinimalApi.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

var stakers = app.MapGroup("/api/stakers");

stakers.MapPost("/", CreateStaker);
stakers.MapGet("/{id}", GetStakerById);

app.Run();

static async Task<IResult> CreateStaker(CreateStakerRequest request, ISender mediatr)
{
    var createStaker = new RegisterStaker(
        Email: request.Email,
        FirstName: request.FirstName,
        LastName: request.LastName,
        Password: request.Password,
        Role: request.Role);

    var newStakerDto = await mediatr.Send(createStaker);
    
    return TypedResults.Created(String.Empty, newStakerDto);
}

static async Task<IResult> GetStakerById(Guid id, ISender mediatr)
{
    var getStakerById = new GetStakerById(id);

    var stakerDto = await mediatr.Send(getStakerById);

    return TypedResults.Ok(stakerDto);
}
