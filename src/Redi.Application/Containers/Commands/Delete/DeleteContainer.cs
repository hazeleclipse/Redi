using MediatR;

namespace Redi.Application.Containers.Commands.Delete
{
    public record DeleteContainer(Guid Id) : IRequest;
}
