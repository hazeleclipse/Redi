using MediatR;

namespace Redi.Application.Containers.Commands.AddChild
{
    public record AddChildContainer(Guid ParentId, Guid ChildId) : IRequest;
}
