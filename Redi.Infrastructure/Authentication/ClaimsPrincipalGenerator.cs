using Redi.Application.Authentication.Queries.Login;
using Redi.Application.Common.Interfaces.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Redi.Infrastructure.Authentication
{
    public class ClaimsPrincipalGenerator : IClaimsPrincipalGenerator
    {
        public ClaimsPrincipal Generate(AuthenticationResult authenticatedIdentity)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, authenticatedIdentity.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, authenticatedIdentity.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, authenticatedIdentity.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Role", authenticatedIdentity.Role)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, "access-token",JwtRegisteredClaimNames.GivenName,"Role");

            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
