using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.EditChild
{
    public class EditChildStakerWeightHandler : IRequestHandler<EditChildStakerWeight>
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IStakerRepository _stakerRepository;

        public EditChildStakerWeightHandler(IContainerRepository containerRepository, IStakerRepository stakerRepository)
        {
            _containerRepository = containerRepository;
            _stakerRepository = stakerRepository;
        }

        public async Task<Unit> Handle(EditChildStakerWeight request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var container = _containerRepository.GetById(request.ContainerId)
                ?? throw new Exception("Container not found");
            var staker = _stakerRepository.GetById(request.StakerId)
                ?? throw new Exception("Staker not found");

            container.UpdateChildStakerWeight(staker, request.Weight);

            _containerRepository.Update(container);

            return Unit.Value;
        }
    }
}
