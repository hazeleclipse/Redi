using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Queries.GetAll
{
    public class GetAllContainersHandler : IRequestHandler<GetAllContainers, List<ContainerDto>>
    {
        private readonly IContainerRepository _containerRepository;

        public GetAllContainersHandler(IContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        public async Task<List<ContainerDto>> Handle(GetAllContainers request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var containers = new List<ContainerDto>();

            _containerRepository.GetAll().ForEach(c =>
            {
                containers.Add(new ContainerDto(
                    c.Id,
                    c.Name,
                    c.Parent?.Id,
                    c.Parent?.Name,
                    c.Stake,
                    c.LocalStake));
            });

            return containers;
        }
    }
}
