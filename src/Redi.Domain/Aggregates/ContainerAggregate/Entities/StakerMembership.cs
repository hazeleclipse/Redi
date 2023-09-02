using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.ContainerAggregate.Entities;

public class StakerMembership : Entity<StakerMembershipId>
{
    public ContainerId ContainerId { get; } = default!;
    public StakerId StakerId { get; } = default!;
    public Stake Stake { get; internal set; } = 0;
    public Stake LocalStake { get; internal set; } = 0;
    public Weight Weight { get; internal set; } = 1;

    public StakerMembership() { }

    public StakerMembership(StakerMembershipId id, ContainerId containerId, StakerId stakerId) : base(id)
    {
        ContainerId = containerId;
        StakerId = stakerId;
    }

    public StakerMembership(StakerMembershipId id, ContainerId containerId, StakerId stakerId, Stake stake,
        Stake localStake, Weight weight) : this(id, containerId, stakerId)
    {
        Stake = stake;
        LocalStake = localStake;
        Weight = weight;
    }
}
