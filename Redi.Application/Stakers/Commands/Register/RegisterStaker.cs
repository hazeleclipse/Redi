using MediatR;

namespace Redi.Application.Stakers.Commands.Register
{
    public record RegisterStaker() : IRequest<StakerDto>;
}
