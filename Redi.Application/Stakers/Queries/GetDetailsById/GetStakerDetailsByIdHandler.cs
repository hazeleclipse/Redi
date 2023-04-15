using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Stakers.Queries.GetDetailsById
{
    public class GetStakerDetailsByIdHandler : IRequestHandler<GetStakerDetailsById, StakerDetailsDto>
    {
        private readonly IStakerRepository _stakerRepository;
        private readonly IContainerRepository _containerRepository;
        private readonly IProfitLedgerRepository _profitLedgerRepository;

        public GetStakerDetailsByIdHandler(IStakerRepository stakerRepository, IContainerRepository containerRepository, IProfitLedgerRepository profitLedgerRepository)
        {
            _stakerRepository = stakerRepository;
            _containerRepository = containerRepository;
            _profitLedgerRepository = profitLedgerRepository;
        }

        public async Task<StakerDetailsDto> Handle(GetStakerDetailsById request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var staker = _stakerRepository.GetById(request.Id)
                ?? throw new Exception($"Staker {request.Id} not found");

            // Get all the containers the staker currently belongs to
            var containers = _containerRepository.GetAll();
            var memberships = new List<MembershipDetailsDto>();

            containers.ForEach(c =>
            {
                var membership = c.Memberships.FirstOrDefault(sm => sm.StakerId == request.Id);

                if (membership is not null)
                    memberships.Add(new MembershipDetailsDto
                        (
                            ContainerName: c.Name,
                            LocalStake: membership.LocalStake,
                            Stake: membership.Stake
                        ));
            });

            // Get all the profits the stakers has accumulated
            var ledger = _profitLedgerRepository.GetFullLedger()
                ?? throw new Exception("Could not get current ledger");
            var profits = new List<ProfitDetailsDto>();

            ledger.StakerProfits
                .Where(entry => entry.StakerId == request.Id)
                .ToList()
                .ForEach(p =>
                {
                    profits.Add(new ProfitDetailsDto
                        (
                            Date: p.Date,
                            Stake: p.Stake,
                            Profit: p.Profit
                        ));
                });

            // Build response
            return new StakerDetailsDto
                (
                    Id: staker.Id,
                    FirstName: staker.FirstName,
                    LastName: staker.LastName,
                    Email: staker.Email,
                    Memberships: memberships,
                    Profits: profits
                );
        }
    }
}
