using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redi.Application.Common.Interfaces.Authentication;
using Redi.Application.Common.Interfaces.Services;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Infrastructure.Authentication;
using Redi.Infrastructure.Persistence.EfCore;
using Redi.Infrastructure.Persistence.EfCore.Repositories;
using Redi.Infrastructure.Services;

namespace Redi.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {

            //services.AddAuth(configuration);

            // Services
            services.AddSingleton<IDateTimeService, DateTimeService>();

            // Repositories
            services.AddDbContext<RediDbContext>(options =>
            {
                options.UseSqlServer("");
            });
            services.AddScoped<IStakerRepository, StakerRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();
            services.AddScoped<IProfitLedgerRepository, ProfitLedgerRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            // For previous JWT Bearer Authentication
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            // Passwords
            services.AddScoped<IRediPasswordHasher, RediPasswordHasher>();
            services.AddScoped<PasswordHasher<Staker>>();


            // Cookie Authentication
            services.AddSingleton<IClaimsPrincipalGenerator, ClaimsPrincipalGenerator>();
            services
                .AddAuthentication(
                    options => { 
                        options.DefaultAuthenticateScheme = "access-token"; })
                .AddCookie(
                    "access-token", 
                    options => {
                        options.Cookie.Name = "access-token";
                        options.ClaimsIssuer = "Redi";
                        options.LoginPath = "/auth/login";
                        options.AccessDeniedPath = "/Error";
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    });

            return services;
        }
    }
}
