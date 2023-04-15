using Redi.Domain.Common.Exceptions;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate.ValueObjects
{
    public class Password : ValueObject
    {
        public Password() { }

        public string Value { get; } = default!;

        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ContainsNullOrWhiteSpaceException<Password>();

            Value = value;
        }

        public static implicit operator Password(string value)
            => new(value);

        public static implicit operator string(Password value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}