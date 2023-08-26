namespace Redi.Domain.Common.Exceptions;

public class StakeOutOfBoundsException : RediException
{
    public StakeOutOfBoundsException() : base("Stake value must be between 0 and 1 inclusive.")
    {
    }
}