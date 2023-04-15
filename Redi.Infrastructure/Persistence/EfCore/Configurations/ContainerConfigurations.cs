using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.ContainerAggregate;
using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    public class ContainerConfigurations : IEntityTypeConfiguration<Container>
    {
        public void Configure(EntityTypeBuilder<Container> builder)
        {
            builder.ToTable(nameof(Container));

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => ContainerId.Create(value)); ;

            builder.Property(c => c.Name)
                .HasMaxLength(100);

            builder.Property(c => c.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(c => c.LocalStake)
                .HasConversion(
                    localStake => localStake.Value,
                    value => new Stake(value));

            builder.Property(c => c.Weight)
                .HasConversion(
                    weight => weight.Value,
                    value => new Weight(value));

            builder.HasMany(c => c.Memberships)
                .WithOne()
                .HasForeignKey(sm => sm.ContainerId);
            builder.Navigation(c => c.Memberships)
                .HasField("_stakerMemberships")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.ChildContainers)
                .WithOne(c => c.Parent)
                .HasForeignKey("ParentId");

            builder.Navigation(c => c.ChildContainers)
                .HasField("_childContainers")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasData(Container.CreateRoot(Guid.NewGuid(), "ROOT"));
        }
    }
}
