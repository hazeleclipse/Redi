using Redi.Domain.Common.Exceptions;
using Redi.Domain.Common.Models;
using System.Runtime.CompilerServices;

namespace Redi.Domain.Common.ValueObjects
{
    public class Stake : ValueObject
    {
        public Stake() { }

        public decimal Value { get; } = 0;

        public Stake(decimal value) 
        {
            if (value > 1 || value < 0)
                throw new StakeOutOfBoundsException();

            Value = value;
        }

        public static implicit operator Stake(decimal value)
            => new(value);

        public static implicit operator decimal(Stake value)
            => value.Value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
