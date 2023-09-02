namespace Redi.Domain.Common.Exceptions;

public class YearOutOfBoundsException : RediException
{
    public YearOutOfBoundsException() : base("Year value must be between 2000 and 9999 inclusive.") { }
}