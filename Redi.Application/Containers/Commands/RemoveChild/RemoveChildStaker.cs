using MediatR;

namespace Redi.Application.Containers.Commands.RemoveChild
{
    public record RemoveChildStaker(Guid ContainerId, Guid StakerId) : IRequest;
}
