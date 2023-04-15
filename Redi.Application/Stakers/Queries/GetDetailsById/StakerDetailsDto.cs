using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Application.Stakers.Queries.GetDetailsById
{
    public record StakerDetailsDto(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        IEnumerable<MembershipDetailsDto> Memberships,
        IEnumerable<ProfitDetailsDto> Profits);
    public record MembershipDetailsDto(string ContainerName, decimal LocalStake, decimal Stake);
    public record ProfitDetailsDto(DateOnly Date, decimal Stake, decimal Profit);
}