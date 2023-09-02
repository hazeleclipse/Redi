using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Stakers.Queries.GetAll
{
    public class GetAllStakersHandler : IRequestHandler<GetAllStakers, List<StakerDto>>
    {
        private readonly IStakerRepository _stakerRepository;

        public GetAllStakersHandler(IStakerRepository stakerRepository)
            => _stakerRepository = stakerRepository;
        
        public async Task<List<StakerDto>> Handle(GetAllStakers request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var stakers = new List<StakerDto>();

            _stakerRepository.GetAll().ForEach(s =>
            {
                stakers.Add(new StakerDto(s.Id));
            });

            return stakers;
        }
    }
}
