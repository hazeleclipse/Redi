using Redi.Domain.Aggregates.NodeAggregate.Entities;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;

namespace Redi.Domain.Aggregates.NodeAggregate
{
    public class ByWeightNode : Node
    {
        readonly HashSet<StakerWeightEntry> _weights = new()
            ;
        public ByWeightNode() { }

        private ByWeightNode(NodeId id, string name, CoreNode? parent) : base(id, name, parent) { }

        public static ByWeightNode Create(NodeId id, string name, CoreNode? parent)
            => new(id, name, parent);

        public IReadOnlyCollection<StakerWeightEntry> Weights => _weights;
    }
}
