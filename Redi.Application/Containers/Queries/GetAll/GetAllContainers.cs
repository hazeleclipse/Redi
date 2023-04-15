using MediatR;

namespace Redi.Application.Containers.Queries.GetAll
{
    public record GetAllContainers() : IRequest<List<ContainerDto>>;
}
