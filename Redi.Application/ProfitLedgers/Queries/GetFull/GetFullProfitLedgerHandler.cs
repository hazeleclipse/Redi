using MediatR;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.ProfitLedgerAggregate;

namespace Redi.Application.ProfitLedgers.Queries.GetFull
{
    public class GetFullProfitLedgerHandler : IRequestHandler<GetFullProfitLedger, ProfitLedger>
    {
        private readonly IProfitLedgerRepository _profitLedgerRepository;

        public GetFullProfitLedgerHandler(IProfitLedgerRepository profitLedgerRepository)
        {
            _profitLedgerRepository = profitLedgerRepository;
        }

        public async Task<ProfitLedger> Handle(GetFullProfitLedger request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            return _profitLedgerRepository.GetFullLedger()
                ?? throw new Exception("Error building Ledger");
        }
    }
}
