using Microsoft.EntityFrameworkCore;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.ProfitLedgerAggregate;

namespace Redi.Infrastructure.Persistence.EfCore.Repositories
{
    public class ProfitLedgerRepository : IProfitLedgerRepository
    {
        private readonly RediDbContext _rediDbContext;

        public ProfitLedgerRepository(RediDbContext rediDbContext)
            => _rediDbContext = rediDbContext;        

        public ProfitLedger? GetByDate(DateOnly date)
        {
            var companyProfitOnDate = _rediDbContext.CompanyProfitEntries.Where(e => e.Date == date).ToList();
            var stakerProfitsOnDate = _rediDbContext.StakerProfitEntries.Where(e => e.Date == date).ToList();

            return ProfitLedger.Create(companyProfitOnDate, stakerProfitsOnDate);
        }

        public ProfitLedger? GetFullLedger()
        {
            return ProfitLedger.Create(_rediDbContext.CompanyProfitEntries.ToList(), _rediDbContext.StakerProfitEntries.ToList());
        }

        public void Update(ProfitLedger ledger)
        {
            foreach (var entry in ledger.CompanyProfitsUpdateQueue)
                if (_rediDbContext.CompanyProfitEntries.Contains(entry))
                    _rediDbContext.Update(entry);
                else
                    _rediDbContext.CompanyProfitEntries.Add(entry);

            foreach (var entry in ledger.StakerProfitsUpdateQueue)
                if (_rediDbContext.StakerProfitEntries.Contains(entry))
                    _rediDbContext.Update(entry);
                else
                    _rediDbContext.StakerProfitEntries.Add(entry);

            _rediDbContext.SaveChanges();
        }
    }
}
