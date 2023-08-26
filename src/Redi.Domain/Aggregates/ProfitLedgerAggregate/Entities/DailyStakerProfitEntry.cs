using Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities
{
    public class DailyStakerProfitEntry : Entity<DailyStakerProfitId>
    {
        public DateOnly Date { get; private set; } = default!;
        public StakerId StakerId { get; private set; } = default!;
        public Stake Stake { get; private set; } = default!;
        public Profit Profit { get; internal set; } = default!;

        public DailyStakerProfitEntry() { }

        internal DailyStakerProfitEntry(DailyStakerProfitId id, DateOnly date, 
            StakerId stakerId, Stake stake, Profit profit) : base(id)
        {
            Date = date;
            StakerId = stakerId;
            Stake = stake;
            Profit = profit;
        }
    }
}
