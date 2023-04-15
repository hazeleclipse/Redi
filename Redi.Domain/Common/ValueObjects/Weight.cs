using Redi.Domain.Common.Models;

namespace Redi.Domain.Common.ValueObjects
{
    public class Weight : ValueObject
    {
        public Weight() { }

        public ushort Value { get; }

        public Weight(ushort value)
           => Value = value;

        public static implicit operator Weight(ushort value)
            => new(value);

        public static implicit operator ushort(Weight value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
