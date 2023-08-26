using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.Delete
{
    public class DeleteContainerHandler : IRequestHandler<DeleteContainer>
    {
        private readonly IContainerRepository _containerRepository;

        public DeleteContainerHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;
        

        public async Task<Unit> Handle(DeleteContainer request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var container = _containerRepository.GetById(request.Id)
                ?? throw new Exception("Container does not exist");

            if (container.Parent is not null)
            {
                var parent = container.Parent;
                parent.RemoveChildContainer(container);
                _containerRepository.Update(parent);
            }

            _containerRepository.DeleteById(request.Id);

            return Unit.Value;
        }
    }
}
