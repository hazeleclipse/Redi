using Redi.Domain.Common.Exceptions;
using Redi.Domain.Common.Models;

namespace Redi.Domain.Aggregates.StakerAggregate.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public EmailAddress() { }

        public string Value { get; } = default!;

        public EmailAddress(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ContainsNullOrWhiteSpaceException<EmailAddress>();

            Value = value;
        }

        public static implicit operator EmailAddress(string value)
            => new(value);

        public static implicit operator string(EmailAddress value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
