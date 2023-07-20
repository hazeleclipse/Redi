using MediatR;
using Redi.Application.Containers.Commands.Create;
using Redi.Application.Containers.Queries.GetAll;

namespace Redi.MinimalApi.Containers
{
    internal static class ContainerHandler
    {
        internal static async Task<IResult> CreateContainer(CreateContainerRequest request, ISender mediatr)
        {
            var createContainer = new CreateContainer(Name: request.Name);

            var newContainerDto = await mediatr.Send(createContainer);

            return TypedResults.Created($"/api/containers/{newContainerDto.Id}", newContainerDto);
        }

        internal static async Task<IResult> GetAllContainers(ISender mediatr)
        {
            var getAllContainers = new GetAllContainers();

            var containers = await mediatr.Send(getAllContainers);

            return TypedResults.Ok(containers);
        }
    }
}
