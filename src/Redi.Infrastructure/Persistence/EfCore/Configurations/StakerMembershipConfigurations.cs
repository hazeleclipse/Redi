using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.ContainerAggregate.Entities;
using Redi.Domain.Aggregates.ContainerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    public class StakerMembershipConfigurations : IEntityTypeConfiguration<StakerMembership>
    {
        public void Configure(EntityTypeBuilder<StakerMembership> builder)
        {
            builder.ToTable(nameof(StakerMembership));

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Id)
                .HasConversion(
                    id => id.Value,
                    value => StakerMembershipId.Create(value));

            builder.Property(sm => sm.ContainerId)
                .HasConversion(
                    id => id.Value,
                    value => ContainerId.Create(value));

            builder.Property(sm => sm.StakerId)
                .HasConversion(
                    id => id.Value,
                    value => StakerId.Create(value));

            builder.Property(sm => sm.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(sm => sm.LocalStake)
                .HasConversion(
                    localStake => localStake.Value,
                    value => new Stake(value));

            builder.Property(sm => sm.Weight)
                .HasConversion(
                    weight => weight.Value,
                    value => new Weight(value));

            builder.HasIndex(sm => sm.StakerId);

        }
    }
}
