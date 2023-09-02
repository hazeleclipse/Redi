using MediatR;

namespace Redi.Application.Stakers.Queries.GetAll
{
    public record GetAllStakers() : IRequest<List<StakerDto>>;
}
