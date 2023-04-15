using MediatR;

namespace Redi.Application.Containers.Commands.EditChild
{
    public record EditChildContainerWeight(
        Guid ParentId,
        Guid ChildId,
        ushort NewWeight) : IRequest;
}
