using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Redi.WebUi.Authorization.IsResourceOwner
{
    public class IsResourceOwnerOrAdminHandler : AuthorizationHandler<IsResourceOwnerOrAdminRequirement, HttpContext>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsResourceOwnerOrAdminRequirement requirement, HttpContext resource)
        {
            if (context.User == null || resource == null)
                return Task.CompletedTask;

            var routeId = resource.GetRouteValue("id");
            var userId = context.User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (routeId == null)
                return Task.CompletedTask;

            if (routeId.ToString() == userId || context.User.IsInRole("Admin"))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
