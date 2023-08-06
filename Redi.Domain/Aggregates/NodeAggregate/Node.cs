using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate
{
    public class Node : AggregateRoot<NodeId>
    {
        public string Name { get; private set; } = default!;
        public CoreNode? Parent { get; internal set; }
        public Stake Stake { get; internal set; } = 0;
        public Stake LocalStake { get; internal set; } = 0;
        public Weight Weight { get; internal set; } = 1;

        public Node() { }

        private protected Node(NodeId id, string name, CoreNode? parent) : base(id)
        {
            Name = name;
            Parent = parent;
        }

        private protected Node(NodeId id, string name, CoreNode? parent,
            Weight weight, Stake localStake, Stake stake) : this(id, name, parent)
        {
            Weight = weight;
            LocalStake = localStake;
            Stake = stake;
        }
    }
}
