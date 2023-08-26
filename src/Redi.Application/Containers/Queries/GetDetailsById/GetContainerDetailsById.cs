using MediatR;

namespace Redi.Application.Containers.Queries.GetDetailsById
{
    public record GetContainerDetailsById(Guid Id) : IRequest<ContainerDetailsDto>;
}
