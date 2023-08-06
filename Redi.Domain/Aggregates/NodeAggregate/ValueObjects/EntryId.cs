using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.NodeAggregate.ValueObjects
{
    public class EntryId : ValueObject
    {
        public EntryId() { }

        public Guid Value { get; }

        private EntryId(Guid id)
            => Value = id;

        public static EntryId Create(Guid id)
            => new(id);

        public static implicit operator EntryId(Guid id)
            => new(id);

        public static implicit operator Guid(EntryId id)
            => id.Value;

        public static implicit operator Guid?(EntryId? id)
        => id?.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
