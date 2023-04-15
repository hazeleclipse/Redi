using Redi.Domain.Aggregates.ProfitLedgerAggregate;

namespace Redi.Application.Persistence
{
    public interface IProfitLedgerRepository
    {       
        ProfitLedger? GetByDate(DateOnly date);
        ProfitLedger? GetFullLedger();
        void Update(ProfitLedger ProfitLedger);        
    }
}
