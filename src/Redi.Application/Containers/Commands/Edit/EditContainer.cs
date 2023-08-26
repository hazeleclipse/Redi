using MediatR;

namespace Redi.Application.Containers.Commands.Edit
{
    public record EditContainer(Guid Id, string Name) : IRequest;
}
