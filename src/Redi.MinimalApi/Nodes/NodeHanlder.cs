using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Redi.Application.Nodes.Commands.Create;
using Redi.Application.Nodes.Commands.Delete;
using Redi.Application.Nodes.Queries.GetAll;
using Redi.Application.Nodes.Queries.GetById;

namespace Redi.MinimalApi.Nodes
{
    internal static class NodeHanlder
    {
        internal static async Task<IResult> CreateNode(CreateNodeRequest request, ISender mediatr)
        {
            var createNode = new CreateNode(Name: request.Name, NodeType: request.NodeType);

            var nodeDto = await mediatr.Send(createNode);

            return TypedResults.Created($"/api/nodes/{nodeDto.Id}", nodeDto);
        }

        internal static async Task<IResult> DeleteNode(Guid id, ISender mediatr)
        {
            var deleteNode = new DeleteNode(Id: id);

            await mediatr.Send(deleteNode);

            return TypedResults.NoContent();
        }

        internal static async Task<IResult> GetAllNodes(ISender mediatr)
        {
            var getAllNodes = new GetAllNodes();

            var nodeDtos = await mediatr.Send(getAllNodes);

            return TypedResults.Ok(nodeDtos);
        }

        internal static async Task<Results<Ok<Application.Nodes.Queries.GetById.NodeDto>, NotFound>> GetNodeById(Guid id, ISender mediatr)
        {
            var getNodeById = new GetNodeById(id);
            
            var nodeDto = await mediatr.Send(getNodeById, CancellationToken.None);

            if (nodeDto is null) return TypedResults.NotFound();

            return TypedResults.Ok(nodeDto);
        }
    }
}
