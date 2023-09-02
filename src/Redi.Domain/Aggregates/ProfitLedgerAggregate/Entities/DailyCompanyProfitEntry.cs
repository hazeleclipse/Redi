using Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities
{
    public class DailyCompanyProfitEntry : Entity<DailyCompanyProfitId>
    {
        public DateOnly Date { get; private set; } = default!;
        public Profit Profit { get; internal set; } = default!;

        public DailyCompanyProfitEntry() { }

        internal DailyCompanyProfitEntry(DailyCompanyProfitId id, DateOnly date, Profit profit) : base(id)
        {
            Date = date;
            Profit = profit;
        }
    }
}
