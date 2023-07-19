using MediatR;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Stakers.Commands.Register
{
    public record RegisterStaker(
        EmailAddress Email,
        FirstName FirstName,
        LastName LastName,
        Password Password,
        string Role ) : IRequest<StakerDto>;
}
