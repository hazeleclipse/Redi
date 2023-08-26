namespace Redi.Domain.Common.Exceptions;

[Serializable]
public class RediException : Exception
{
    public RediException(string message) : base(message) { }
}
