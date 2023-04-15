using Redi.Application.Authentication.Queries.Login;
using System.Security.Claims;

namespace Redi.Application.Common.Interfaces.Authentication
{
    public interface IClaimsPrincipalGenerator
    {
        ClaimsPrincipal Generate(AuthenticationResult  authenticatedIdentity);
    }
}
