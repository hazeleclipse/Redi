using MediatR;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Enumerations;

namespace Redi.Application.Stakers.Commands.Edit
{
    public record UpdateStakerDetails(
        StakerId Id,
        EmailAddress Email,
        FirstName FirstName,
        LastName LastName,
        string Role) : IRequest;
}
