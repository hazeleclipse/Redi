using MediatR;
using Redi.Application.Persistence;

namespace Redi.Application.Containers.Queries.GetDetailsById
{
    public class GetContainerDetailsByIdHandler : IRequestHandler<GetContainerDetailsById, ContainerDetailsDto>
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IStakerRepository _stakerRepository;

        public GetContainerDetailsByIdHandler(IContainerRepository containerRepository, IStakerRepository stakerRepository)
        {
            _containerRepository = containerRepository;
            _stakerRepository = stakerRepository;
        }

        public async Task<ContainerDetailsDto> Handle(GetContainerDetailsById request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var container = _containerRepository.GetById(request.Id)
                ?? throw new Exception($"Could not find container with Id: {request.Id}.");

            // Child Containers
            var childContainers = new List<ChildContainerDto>();
            
            foreach (var child in container.ChildContainers)
            {
                childContainers.Add(new ChildContainerDto
                    (
                        Id: child.Id,
                        Name: child.Name,
                        Stake: child.Stake,
                        LocalStake: child.LocalStake,
                        Weight: child.Weight
                    ));
            }

            // Member Stakers
            var members = new List<MemberStakerDto>();
            var stakerIds = container.Memberships.Select(x => x.StakerId).ToList();
            
            _stakerRepository
                .GetRange(stakerIds)
                .ForEach(s =>
                {
                    // This error should never be thrown since we are using the id list we just pulled from the container.
                    var membership = container.Memberships.FirstOrDefault(m => m.StakerId == s.Id)
                        ?? throw new Exception($"Could not find membership for staker {s.Id} in container {container.Id}.");

                    members.Add(new MemberStakerDto
                        (
                            s.Id,
                            s.FirstName,
                            s.LastName,
                            membership.Stake,
                            membership.LocalStake,
                            membership.Weight
                        ));        
                });


            return new ContainerDetailsDto
                (
                    container.Id,
                    container.Name,
                    container.Parent?.Id,
                    container.Parent?.Name,
                    container.Stake,
                    container.LocalStake,
                    childContainers,
                    members
                );
        }
    }
}
