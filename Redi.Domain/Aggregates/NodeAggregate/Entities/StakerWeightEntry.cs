using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate.Entities
{
    public class StakerWeightEntry : Entity<EntryId>
    {
        public NodeId NodeId { get; } = default!;
        public StakerId StakerId { get; } = default!;
        public Stake Stake { get; internal set; } = 0;
        public Stake LocalStake { get; internal set; } = 0;
        public Weight Weight { get; internal set; } = 1;

        public StakerWeightEntry() { }

        public StakerWeightEntry(EntryId id, NodeId nodeId, StakerId stakerId) : base(id)
        {
            NodeId = nodeId;
            StakerId = stakerId;
        }

        public StakerWeightEntry(EntryId id, NodeId nodeId, StakerId stakerId, Stake stake,
            Stake localStake, Weight weight) : this(id, nodeId, stakerId)
        {
            Stake = stake;
            LocalStake = localStake;
            Weight = weight;
        }
    }
}
