using MediatR;

namespace Redi.Application.Containers.Commands.AddChild
{
    public record AddChildStaker(Guid ContainerId, Guid StakerId) : IRequest;
}
