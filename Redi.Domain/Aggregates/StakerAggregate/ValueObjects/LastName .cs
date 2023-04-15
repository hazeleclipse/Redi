using Redi.Domain.Common.Exceptions;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate.ValueObjects
{
    public class LastName : ValueObject
    {
        public LastName() { }

        public string Value { get; } = default!;

        public LastName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ContainsNullOrWhiteSpaceException<LastName>();

            Value = value;
        }

        public static implicit operator LastName(string value)
            => new(value);

        public static implicit operator string(LastName value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
