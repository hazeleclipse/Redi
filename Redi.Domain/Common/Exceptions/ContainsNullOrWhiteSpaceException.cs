using Redi.Domain.Common.Models;

namespace Redi.Domain.Common.Exceptions;

public class ContainsNullOrWhiteSpaceException<T> : RediException
    where T : ValueObject
{
    public ContainsNullOrWhiteSpaceException() : base($"{typeof(T).Name} cannot be null or contain only whitespace.") { }
}