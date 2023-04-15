using Redi.Domain.Common.Exceptions;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate.ValueObjects
{
    public class FirstName : ValueObject
    {
        public FirstName() { }

        public string Value { get; } = default!;

        public FirstName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ContainsNullOrWhiteSpaceException<FirstName>();

            Value = value;
        }

        public static implicit operator FirstName(string value)
            => new(value);

        public static implicit operator string(FirstName value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
