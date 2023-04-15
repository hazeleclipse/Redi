using MediatR;

namespace Redi.Application.Containers.Commands.RemoveChild
{
    public record RemoveChildContainer(Guid ParentId, Guid ChildId) : IRequest;
}
