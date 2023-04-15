using Redi.Domain.Common.Models;

namespace Redi.Domain.Common.ValueObjects
{
    public class Profit : ValueObject
    {
        public Profit() { }

        public decimal Value { get; } = 0;

        public Profit(decimal value)
        {
            Value = value;
        }

        public static implicit operator Profit(decimal value)
            => new(value);

        public static implicit operator decimal(Profit value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
