using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Infrastructure.Persistence.EfCore.Configurations
{
    public class DailyCompanyProfitEntryConfigurations : IEntityTypeConfiguration<DailyCompanyProfitEntry>
    {
        public void Configure(EntityTypeBuilder<DailyCompanyProfitEntry> builder)
        {
            builder.ToTable(nameof(DailyCompanyProfitEntry));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => DailyCompanyProfitId.Create(value));

            builder.Property(x => x.Date);

            builder.Property(x => x.Profit)
                .HasConversion(
                    profit => profit.Value,
                    value => new Profit(value));

            builder.HasIndex(x => x.Date).IsUnique();
        }
    }
}
