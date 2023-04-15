using MediatR;

namespace Redi.Application.Authentication.Queries.Login
{
    public record class LoginQuery(
        string Email,
        string Password) : IRequest<AuthenticationResult>;
}
