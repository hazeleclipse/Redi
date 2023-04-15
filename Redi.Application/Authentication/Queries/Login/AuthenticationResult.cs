namespace Redi.Application.Authentication.Queries.Login
{
    public record AuthenticationResult(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        string Role);
}