using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Redi.Application.Nodes.Commands.Update;
using Redi.Application.Nodes.Queries.GetById;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.MinimalApi.Nodes;

namespace Redi.MinimalApi.Tests.Unit;

public class NodeRoutesTests
{
    [Fact]
    public async void GetNodeById_EntityExists_ReturnOkAndSpecifiedNodeDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var getNodeById = new GetNodeById(id);

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(getNodeById, CancellationToken.None))
            .ReturnsAsync(new NodeDto(id, "Node from Moq", "NodeType"));

        // Act
        var result = await NodeHanlder.GetNodeById(id, mockMediator.Object);

        // Assert
        Assert.IsType<Results<Ok<NodeDto>, NotFound>>(result);

        var okResult = (Ok<NodeDto>)result.Result;

        Assert.NotNull(okResult);
        Assert.IsType<NodeDto>(okResult.Value);
        Assert.Equal(id, okResult.Value?.Id);        
    }

    [Fact]
    public async void GetNodeById_EntityDoesNotExist_ReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var getNodeById = new GetNodeById(id);

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(getNodeById, CancellationToken.None))
            .ReturnsAsync((NodeDto?)null);

        // Act
        var result = await NodeHanlder.GetNodeById(id, mockMediator.Object);

        // Assert
        Assert.IsType<Results<Ok<NodeDto>, NotFound>>(result);

        var notFoundResult = (NotFound) result.Result;

        Assert.NotNull(notFoundResult);       
    }

    [Fact]
    public async void UpdateNode_NodeExists_ReturnNoContent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var targetNode = CoreNode.Create(id, "target", null);
        var updateNode = new UpdateNode(targetNode.Id , "new name");

        var updateNodeRequest = new UpdateNodeRequest("new name");

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(updateNode, CancellationToken.None))
            .Verifiable();

        // Act
        var result = await NodeHanlder.UpdateNode(id, updateNodeRequest, mockMediator.Object);

        // Assert
        Assert.IsType<Results<NoContent, NotFound>>(result);
        var noContentResult = result.Result as NoContent;
        Assert.NotNull(noContentResult);
    }

    [Fact]
    public async void UpdateNode_NodeDoesNotExist_ReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var targetNode = CoreNode.Create(id, "target", null);
        var updateNode = new UpdateNode(targetNode.Id , "new name");

        var updateNodeRequest = new UpdateNodeRequest("new name");

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(updateNode, CancellationToken.None))
            .Throws(new KeyNotFoundException($"Node {id} was not found"))
            .Verifiable();

        // Act
        var result = await NodeHanlder.UpdateNode(id, updateNodeRequest, mockMediator.Object);
        
        // Assert
        Assert.IsType<Results<NoContent, NotFound>>(result);
        var notFoundResult = result.Result as NotFound;
        Assert.NotNull(notFoundResult);
    }
}

