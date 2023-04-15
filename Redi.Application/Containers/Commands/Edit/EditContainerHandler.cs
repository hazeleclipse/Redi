using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Commands.Edit
{
    public class EditContainerHandler : IRequestHandler<EditContainer>
    {
        private readonly IContainerRepository _containerRepository;

        public EditContainerHandler(IContainerRepository containerRepository)
            => _containerRepository = containerRepository;

        public async Task<Unit> Handle(EditContainer request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var container = _containerRepository.GetById(request.Id)
                ?? throw new Exception("Container not found");

            container.Edit(request.Name);

            _containerRepository.Update(container);

            return Unit.Value;
        }
    }
}
