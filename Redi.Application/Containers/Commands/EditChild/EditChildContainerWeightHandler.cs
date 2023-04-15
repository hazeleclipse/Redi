using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.EditChild
{
    public class EditChildContainerWeightHandler : IRequestHandler<EditChildContainerWeight>
    {
        private readonly IContainerRepository _containerRepository;

        public EditChildContainerWeightHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;

        public async Task<Unit> Handle(EditChildContainerWeight request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var parentContainer = _containerRepository.GetById(request.ParentId)
                ?? throw new Exception("Target Container not found");
            var childContainer = _containerRepository.GetById(request.ChildId)
                ?? throw new Exception("Child Container not found");

            parentContainer.UpdateChildContainerWeight(childContainer, request.NewWeight);

            _containerRepository.Update(parentContainer);

            return Unit.Value;
        }
    }
}
