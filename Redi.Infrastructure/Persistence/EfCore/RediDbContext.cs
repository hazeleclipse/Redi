using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Redi.Domain.Aggregates.ContainerAggregate;
using Redi.Domain.Aggregates.ContainerAggregate.Entities;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities;
using Redi.Domain.Aggregates.StakerAggregate;

namespace Redi.Infrastructure.Persistence.EfCore
{
    public class RediDbContext : DbContext
    {
        public RediDbContext(DbContextOptions<RediDbContext> options) : base(options) { }

        public DbSet<DailyCompanyProfitEntry> CompanyProfitEntries { get; set; } = default!;
        public DbSet<DailyStakerProfitEntry> StakerProfitEntries { get; set; } = default!;
        public DbSet<Container> Containers { get; set; } = default!;
        public DbSet<StakerMembership> StakerMemberships { get; set; } = default!;
        public DbSet<Staker> Stakers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(RediDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        /// <summary>
        /// Converts <see cref="DateOnly" /> to <see cref="DateTime"/> and vice versa.
        /// </summary>
        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            /// <summary>
            /// Creates a new instance of this converter.
            /// </summary>
            public DateOnlyConverter() : base(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d))
            { }
        }
    }
}
