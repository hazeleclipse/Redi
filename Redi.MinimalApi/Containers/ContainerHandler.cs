using MediatR;
using Redi.Application.Containers.Commands.Create;
using Redi.Application.Containers.Queries.GetAll;
using Redi.Application.Containers.Queries.GetDetailsById;

namespace Redi.MinimalApi.Containers
{
    internal static class ContainerHandler
    {
        internal static async Task<IResult> CreateContainer(CreateContainerRequest request, ISender mediatr)
        {
            var createContainer = new CreateContainer(Name: request.Name);

            var containerDto = await mediatr.Send(createContainer);

            return TypedResults.Created($"/api/containers/{containerDto.Id}", containerDto);
        }

        internal static async Task<IResult> GetAllContainers(ISender mediatr)
        {
            var getAllContainers = new GetAllContainers();

            var containerDtos = await mediatr.Send(getAllContainers);

            return TypedResults.Ok(containerDtos);
        }

        internal static async Task<IResult> GetContainerById(Guid id, ISender mediatr)
        {
            var getContainerById = new GetContainerDetailsById(id);

            var containerDetailsDto = await mediatr.Send(getContainerById);

            return TypedResults.Ok(containerDetailsDto);
        }
    }
}
