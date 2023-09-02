using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.AddChild
{
    public class AddChildStakerHandler : IRequestHandler<AddChildStaker>
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IStakerRepository _stakerRepository;

        public AddChildStakerHandler(IContainerRepository containerRepository, IStakerRepository stakerRepository)
        {
            _containerRepository = containerRepository;
            _stakerRepository = stakerRepository;
        }

        public async Task<Unit> Handle(AddChildStaker request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var container = _containerRepository.GetById(request.ContainerId)
                ?? throw new Exception("Container not found");

            var staker = _stakerRepository.GetById(request.StakerId)
                ?? throw new Exception("Staker not found");

            container.AddChildStaker(staker);

            _containerRepository.Update(container);

            return Unit.Value;
        }
    }
}
