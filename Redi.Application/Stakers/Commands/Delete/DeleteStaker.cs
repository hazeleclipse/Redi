using MediatR;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Stakers.Commands.Delete
{
    public record DeleteStaker(StakerId Id) : IRequest;
}
