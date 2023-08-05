using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Redi.Application.Common.Interfaces.Services;
using Redi.Application.Persistence;
using Redi.Infrastructure.Persistence.EfCore;
using Redi.Infrastructure.Persistence.EfCore.Repositories;
using Redi.Infrastructure.Services;

namespace Redi.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {

            // Services
            services.AddSingleton<IDateTimeService, DateTimeService>();

            // Repositories
            services.AddDbContext<RediDbContext>(options =>
            {
                options.UseSqlServer("Server=tcp:(local);Initial Catalog=NewRediDb;Authentication=Sql Password;User=sa;Password=Germany@39;TrustServerCertificate=true");
            });
            services.AddScoped<IStakerRepository, StakerRepository>();
            services.AddScoped<IContainerRepository, ContainerRepository>();
            services.AddScoped<IProfitLedgerRepository, ProfitLedgerRepository>();
            services.AddScoped<INodeRepository, NodeRepository>();

            return services;
        }
    }
}
