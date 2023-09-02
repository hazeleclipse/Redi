using MediatR;

namespace Redi.Application.Containers.Commands.EditChild
{
    public record EditChildStakerWeight(Guid ContainerId, Guid StakerId, ushort Weight) : IRequest;
}
