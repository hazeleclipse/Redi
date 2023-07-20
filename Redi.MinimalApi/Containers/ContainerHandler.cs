using MediatR;
using Redi.Application.Containers.Commands.AddChild;
using Redi.Application.Containers.Commands.Create;
using Redi.Application.Containers.Commands.Delete;
using Redi.Application.Containers.Commands.Edit;
using Redi.Application.Containers.Queries.GetAll;
using Redi.Application.Containers.Queries.GetDetailsById;

namespace Redi.MinimalApi.Containers
{
    internal static class ContainerHandler
    {
        internal static async Task<IResult> AddChildContainer(Guid id, Guid childId, ISender mediatr)
        {
            var addChildContainer = new AddChildContainer(id, childId);

            await mediatr.Send(addChildContainer);

            return TypedResults.Ok();
        }

        internal static async Task<IResult> CreateContainer(CreateContainerRequest request, ISender mediatr)
        {
            var createContainer = new CreateContainer(Name: request.Name);

            var containerDto = await mediatr.Send(createContainer);

            return TypedResults.Created($"/api/containers/{containerDto.Id}", containerDto);
        }

        internal static async Task<IResult> DeleteContainer(Guid id, ISender mediatr)
        {
            var deleteContainer = new DeleteContainer(id);

            await mediatr.Send(deleteContainer);

            return TypedResults.NoContent();
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

        internal static async Task<IResult> UpdateContainer(UpdateContainerRequest request, Guid id,  ISender mediatr)
        {
            var editContainer = new EditContainer(Id: id, Name: request.Name);
            
            await mediatr.Send(editContainer);

            return TypedResults.NoContent();
        }
    }
}
