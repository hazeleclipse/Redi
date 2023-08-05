using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.NodeAggregate.Entities;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    internal class WeightEntryConfigurations : IEntityTypeConfiguration<StakerWeightEntry>
    {
        public void Configure(EntityTypeBuilder<StakerWeightEntry> builder)
        {
            builder.ToTable(nameof(StakerWeightEntry));

            builder.HasKey(sm => sm.Id);

            builder.Property(sm => sm.Id)
                .HasConversion(
                    id => id.Value,
                    value => EntryId.Create(value));

            builder.Property(sm => sm.NodeId)
                .HasConversion(
                    id => id.Value,
                    value => NodeId.Create(value));

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
