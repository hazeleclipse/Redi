using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.StakerAggregate;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    internal class StakerConfigurations : IEntityTypeConfiguration<Staker>
    {
        public void Configure(EntityTypeBuilder<Staker> builder)
        {
            builder.ToTable(nameof(Staker));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => StakerId.Create(value));

            builder.HasData(Staker.Create(Guid.NewGuid()));
        }
    }
}
