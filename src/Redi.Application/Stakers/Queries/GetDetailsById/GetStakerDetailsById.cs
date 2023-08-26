using MediatR;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Stakers.Queries.GetDetailsById
{
    public record GetStakerDetailsById(StakerId Id) : IRequest<StakerDetailsDto>;
}
