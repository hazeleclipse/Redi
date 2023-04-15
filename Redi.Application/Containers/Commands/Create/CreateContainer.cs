using MediatR;

namespace Redi.Application.Containers.Commands.Create
{
    public record CreateContainer(string Name) : IRequest;
}
