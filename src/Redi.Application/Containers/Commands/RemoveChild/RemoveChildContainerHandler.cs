using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.RemoveChild
{
    public class RemoveChildContainerHandler : IRequestHandler<RemoveChildContainer>
    {
        private readonly IContainerRepository _containerRepository;

        public RemoveChildContainerHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;

        public async Task<Unit> Handle(RemoveChildContainer request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var parentContainer = _containerRepository.GetById(request.ParentId)
                ?? throw new Exception("Target Container not found");
            var childContainer = _containerRepository.GetById(request.ChildId)
                ?? throw new Exception("Child Container not found");

            parentContainer.RemoveChildContainer(childContainer);

            _containerRepository.Update(parentContainer);
            _containerRepository.Update(childContainer);

            return Unit.Value;
        }
    }
}
