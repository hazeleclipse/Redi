using MediatR;
using Redi.Application.Common.Interfaces.Services;
using Redi.Application.Persistence;

namespace Redi.Application.ProfitLedgers.Commands.CalculateEndOfDayProfits
{
    public class CalculateEndOfDayProfitsHandler : IRequestHandler<CalculateEndOfDayProfits>
    {
        private readonly IProfitLedgerRepository _profitLedgerRepository;
        private readonly IContainerRepository _containerRepository;
        private readonly IDateTimeService _dateTimeService;

        public CalculateEndOfDayProfitsHandler(IContainerRepository containerRepository, IProfitLedgerRepository profitLedgerRepository, IDateTimeService dateTimeService)
        {
            _containerRepository = containerRepository;
            _profitLedgerRepository = profitLedgerRepository;
            _dateTimeService = dateTimeService;
        }

        public async Task<Unit> Handle(CalculateEndOfDayProfits request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var rootContainer = _containerRepository.GetRoot()
                ?? throw new Exception("Root container not found");

            var ledger = _profitLedgerRepository.GetFullLedger()
                ?? throw new Exception("Current ledger not found");

            ledger.AddDaliyProift(rootContainer.GetStakerTotalStakes(), request.Date, request.Profit);

            _profitLedgerRepository.Update(ledger);

            return Unit.Value;
        }
    }
}
