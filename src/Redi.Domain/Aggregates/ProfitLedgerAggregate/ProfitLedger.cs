using Redi.Domain.Aggregates.ProfitLedgerAggregate.Entities;
using Redi.Domain.Aggregates.ProfitLedgerAggregate.ValueObjects;
using Redi.Domain.Aggregates.StakerAggregate.ValueObjects;
using Redi.Domain.Common.Models;
using Redi.Domain.Common.ValueObjects;

namespace Redi.Domain.Aggregates.ProfitLedgerAggregate
{
    public class ProfitLedger
    {
        private readonly List<DailyCompanyProfitEntry> _dailyCompanyProfits = new();
        private readonly List<DailyStakerProfitEntry> _dailyStakerProfits = new();
        private readonly Queue<DailyCompanyProfitEntry> _companyProfitUpdateQueue = new();
        private readonly Queue<DailyStakerProfitEntry> _stakerProfitUpdateQueue = new();

        public ProfitLedger() { }

        private ProfitLedger(List<DailyCompanyProfitEntry> dailyCompanyProfits, List<DailyStakerProfitEntry> dailyStakerProfits)
        {
            _dailyCompanyProfits = dailyCompanyProfits;
            _dailyStakerProfits = dailyStakerProfits;
        }

        public IReadOnlyList<DailyCompanyProfitEntry> CompanyProfits => _dailyCompanyProfits.AsReadOnly();
        public IReadOnlyList<DailyStakerProfitEntry> StakerProfits => _dailyStakerProfits.AsReadOnly();
        public IReadOnlyList<DailyCompanyProfitEntry> CompanyProfitsUpdateQueue => _dailyCompanyProfits.AsReadOnly();
        public IReadOnlyList<DailyStakerProfitEntry> StakerProfitsUpdateQueue => _dailyStakerProfits.AsReadOnly();

        public void AddDaliyProift(IEnumerable<KeyValuePair<StakerId, Stake>> stakersStakes, DateOnly date, Profit profit)
        {
            if (_dailyCompanyProfits.Any(dcp => dcp.Date == date))
                throw new Exception($"There is already a profit entry for {date}. You must edit that entry.");

            var entry = new DailyCompanyProfitEntry(
                Guid.NewGuid(),
                date,
                profit);

            _dailyCompanyProfits.Add(entry);
            _companyProfitUpdateQueue.Enqueue(entry);

            CreateDailyStakerProfitEntries(stakersStakes, date);
        }

        public static ProfitLedger Create()
        {
            return new();
        }

        public static ProfitLedger Create(List<DailyCompanyProfitEntry> dailyCompanyProfits, List<DailyStakerProfitEntry> dailyStakerProfits)
        {
            return new(dailyCompanyProfits, dailyStakerProfits);
        }

        public void UpdateDailyProfit(DateOnly date, Profit profit)
        {
            var dailyCompanyProfit = _dailyCompanyProfits.Find(dcp => dcp.Date == date)
                ?? throw new Exception("No entry for that date in this ledger");

            dailyCompanyProfit.Profit = profit;

            _companyProfitUpdateQueue.Enqueue(dailyCompanyProfit);
            UpdateDailyStakerProfitEntries(date);
        }

        private void CreateDailyStakerProfitEntries(IEnumerable<KeyValuePair<StakerId, Stake>> stakersStakes, DateOnly date)
        {
            var profit = _dailyCompanyProfits.Where(dcp => dcp.Date == date).Select(dcp => dcp.Profit).FirstOrDefault()
                ?? throw new Exception($"No Profit found for date {date}.");

            foreach (var stakersStake in stakersStakes)
                if(stakersStake.Value != 0M)
                {
                   var entry = new DailyStakerProfitEntry(
                        Guid.NewGuid(),
                        date,
                        stakersStake.Key,
                        stakersStake.Value,
                        profit * stakersStake.Value);

                    _dailyStakerProfits.Add(entry);
                    _stakerProfitUpdateQueue.Enqueue(entry);
                }

        }

        private void UpdateDailyStakerProfitEntries(DateOnly date)
        {
            var profit = _dailyCompanyProfits.Where(dcp => dcp.Date == date).Select(dcp => dcp.Profit).FirstOrDefault()
                ?? throw new Exception($"No Profit found for date {date}.");

            var dailyStakerProfitEntries = _dailyStakerProfits.Where(e => e.Date == date).ToList();

            dailyStakerProfitEntries.ForEach(e =>
            {
                e.Profit = e.Stake * profit;
                _stakerProfitUpdateQueue.Enqueue(e);
            });
        }
    }
}
