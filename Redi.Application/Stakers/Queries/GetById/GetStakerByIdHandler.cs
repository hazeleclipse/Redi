using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Stakers.Queries.GetById
{
    public class GetStakerByIdHandler : IRequestHandler<GetStakerById, StakerDto>
    {
        private readonly IStakerRepository _stakerRepository;

        public GetStakerByIdHandler(IStakerRepository stakerRepository)
            => _stakerRepository = stakerRepository;

        public async Task<StakerDto> Handle(GetStakerById request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var staker = _stakerRepository.GetById(request.Id)
                ?? throw new Exception("Staker not found.");

            return new StakerDto(
                staker.Id,
                staker.Email,
                staker.FirstName,
                staker.LastName,
                staker.Role.ToString());
        }
    }
}
