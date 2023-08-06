using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    internal class NodeConfigurations : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.ToTable(nameof(Node));

            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => NodeId.Create(value)); ;

            builder.Property(n => n.Name)
                .HasMaxLength(100);

            builder.Property(n => n.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(n => n.LocalStake)
                .HasConversion(
                    localStake => localStake.Value,
                    value => new Stake(value));

            builder.Property(n => n.Weight)
                .HasConversion(
                    weight => weight.Value,
                    value => new Weight(value));
        }
    }

    internal class CoreNodeConfigurations : IEntityTypeConfiguration<CoreNode>
    {
        public void Configure(EntityTypeBuilder<CoreNode> builder)
        {
            builder.ToTable(nameof(Node));

            builder.Property(n => n.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => NodeId.Create(value)); ;

            builder.Property(n => n.Name)
                .HasMaxLength(100);

            builder.Property(n => n.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(n => n.LocalStake)
                .HasConversion(
                    localStake => localStake.Value,
                    value => new Stake(value));

            builder.Property(n => n.Weight)
                .HasConversion(
                    weight => weight.Value,
                    value => new Weight(value));

            builder.HasMany(n => n.Children)
                .WithOne(n => n.Parent)
                .HasForeignKey("ParentId");

            builder.Navigation(n => n.Children)
                .HasField("_children")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }

    internal class ByWeightNodeConfigurations : IEntityTypeConfiguration<ByWeightNode>
    {
        public void Configure(EntityTypeBuilder<ByWeightNode> builder)
        {
            builder.ToTable(nameof(Node));

            builder.Property(n => n.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => NodeId.Create(value)); ;

            builder.Property(n => n.Name)
                .HasMaxLength(100);

            builder.Property(n => n.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(n => n.LocalStake)
                .HasConversion(
                    localStake => localStake.Value,
                    value => new Stake(value));

            builder.Property(n => n.Weight)
                .HasConversion(
                    weight => weight.Value,
                    value => new Weight(value));

            builder.HasMany(n => n.Weights)
                .WithOne()
                .HasForeignKey(sw => sw.NodeId);

            builder.Navigation(n => n.Weights)
                .HasField("_weights")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
