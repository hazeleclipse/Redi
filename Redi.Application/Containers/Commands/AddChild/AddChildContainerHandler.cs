using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.AddChild
{
    public class AddChildContainerHandler : IRequestHandler<AddChildContainer>
    {
        private readonly IContainerRepository _containerRepository;

        public AddChildContainerHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;
        
        public async Task<Unit> Handle(AddChildContainer request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var targetContainer = _containerRepository.GetById(request.ParentId)
                ?? throw new Exception("Target Container not found");

            var newChildContainer = _containerRepository.GetById(request.ChildId)
                ?? throw new Exception("Child Container not found");

            targetContainer.AddChildContainer(newChildContainer);

            _containerRepository.Update(targetContainer);

            return Unit.Value;
        }
    }
}
