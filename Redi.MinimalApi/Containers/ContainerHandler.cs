using MediatR;
using Redi.Application.Containers.Commands.AddChild;
using Redi.Application.Containers.Commands.Create;
using Redi.Application.Containers.Commands.Delete;
using Redi.Application.Containers.Commands.Edit;
using Redi.Application.Containers.Commands.EditChild;
using Redi.Application.Containers.Commands.RemoveChild;
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

        internal static async Task<IResult> AddChildStaker(Guid id, Guid childId, ISender mediatr)
        {
            var addChildStaker = new AddChildStaker(id, childId);

            await mediatr.Send(addChildStaker);

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

        internal static async Task<IResult> EditChildContainer(Guid id, Guid childId, EditChildWeightRequest request,  ISender mediatr)
        {
            var editChildContainer = new EditChildContainerWeight(id, childId, request.Weight);

            await mediatr.Send(editChildContainer);

            return TypedResults.Ok();
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

        internal static async Task<IResult> RemoveChildContainer(Guid id, Guid childId, ISender mediatr)
        {
            var removeChildContainer = new RemoveChildContainer(id, childId);

            await mediatr.Send(removeChildContainer);

            return TypedResults.Ok();
        }

        internal static async Task<IResult> RemoveChildStaker(Guid id, Guid childId, ISender mediatr)
        {
            var removeChildStaker = new RemoveChildStaker(id, childId);

            await mediatr.Send(removeChildStaker);

            return TypedResults.Ok();
        }

        internal static async Task<IResult> UpdateContainer(UpdateContainerRequest request, Guid id,  ISender mediatr)
        {
            var editContainer = new EditContainer(Id: id, Name: request.Name);
            
            await mediatr.Send(editContainer);

            return TypedResults.NoContent();
        }
    }
}
