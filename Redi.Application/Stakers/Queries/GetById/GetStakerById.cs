using MediatR;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Application.Stakers.Queries.GetById
{
    public record GetStakerById(StakerId Id) : IRequest<StakerDto>;
}
