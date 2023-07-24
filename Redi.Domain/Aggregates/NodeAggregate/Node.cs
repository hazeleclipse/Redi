using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate
{
    public abstract class Node : AggregateRoot<NodeId>
    {
        public string Name { get; private set; } = default!;
        public Node? Parent { get; private set; }
        public Stake Stake { get; private set; } = 0;
        public Stake LocalStake { get; private set; } = 0;
        public Weight Weight { get; private set; } = 1;

        public Node() { }
    }
}
