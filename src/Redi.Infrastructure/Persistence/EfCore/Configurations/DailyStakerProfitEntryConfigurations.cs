using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    public class DailyStakerProfitEntryConfigurations : IEntityTypeConfiguration<DailyStakerProfitEntry>
    {
        public void Configure(EntityTypeBuilder<DailyStakerProfitEntry> builder)
        {
            builder.ToTable(nameof(DailyStakerProfitEntry));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => DailyStakerProfitId.Create(value));

            builder.Property(x => x.Date);

            builder.Property(x => x.StakerId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => StakerId.Create(value));

            builder.Property(sm => sm.Stake)
                .HasConversion(
                    stake => stake.Value,
                    value => new Stake(value));

            builder.Property(x => x.Profit)
                .HasConversion(
                    profit => profit.Value,
                    value => new Profit(value));

            builder.HasIndex(x => x.Date);
        }
    }
}
