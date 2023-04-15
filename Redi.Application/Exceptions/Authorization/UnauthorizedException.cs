namespace Redi.Application.Exceptions.Authorization
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
