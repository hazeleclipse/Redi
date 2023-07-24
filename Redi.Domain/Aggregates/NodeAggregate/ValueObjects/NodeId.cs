using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.NodeAggregate.ValueObjects
{
    public class NodeId : ValueObject
    {
        public NodeId() { }

        public Guid Value { get; }

        private NodeId(Guid id)
            => Value = id;

        public static NodeId Create(Guid id)
            => new(id);

        public static implicit operator NodeId(Guid id)
            => new(id);

        public static implicit operator Guid(NodeId id)
            => id.Value;

        public static implicit operator Guid?(NodeId? id)
        => id?.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
