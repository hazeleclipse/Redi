using Microsoft.AspNetCore.Authorization;
using Redi.WebUi.Authorization.IsAdmin;
using Redi.WebUi.Authorization.IsResourceOwner;

namespace Redi.WebUi.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebUi(this IServiceCollection services)
        {

            services.AddRazorPages();
            services.AddAuth();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new IAuthorizationRequirement[] { new IsAdminRequirement() })
                    .Build();

                options.AddPolicy("ResourceOwnerOrAdmin", policy =>
                    policy.Requirements.Add(new IsResourceOwnerOrAdminRequirement()));   
            });

            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
            services.AddSingleton<IAuthorizationHandler, IsResourceOwnerOrAdminHandler>();

            return services;
        }
    }
}
