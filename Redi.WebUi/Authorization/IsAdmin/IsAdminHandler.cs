using Microsoft.AspNetCore.Authorization;

namespace Redi.WebUi.Authorization.IsAdmin
{
    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.IsInRole("Admin"))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
