using System.Reflection.Metadata.Ecma335;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Redi.Application.Nodes.Queries.GetById;
using Redi.MinimalApi;
using Redi.MinimalApi.Nodes;

namespace Redi.MinimalApi.Tests.Unit;

public class NodeRoutesTests
{
    [Fact]
    public async void GetById_EntityExists_ReturnOkAndSpecifiedNodeDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var getNodeById = new GetNodeById(id);

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(getNodeById, CancellationToken.None))
            .ReturnsAsync(new NodeDto(id, "Node from Moq", "NodeType"));

        // Act
        var result = await NodeHanlder.GetById(id, mockMediator.Object);

        // Assert
        Assert.IsType<Results<Ok<NodeDto>, NotFound>>(result);

        var okResult = (Ok<NodeDto>)result.Result;

        Assert.NotNull(okResult);
        Assert.IsType<NodeDto>(okResult.Value);
        Assert.Equal(id, okResult.Value?.Id);        
    }

    [Fact]
    public async void GetById_EntityDoesNotExist_ReturnNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var getNodeById = new GetNodeById(id);

        var mockMediator = new Mock<ISender>();
        mockMediator
            .Setup(m => m.Send(getNodeById, CancellationToken.None))
            .ReturnsAsync((NodeDto?)null);

        // Act
        var result = await NodeHanlder.GetById(id, mockMediator.Object);

        // Assert
        Assert.IsType<Results<Ok<NodeDto>, NotFound>>(result);

        var notFoundResult = (NotFound) result.Result;

        Assert.NotNull(notFoundResult);       
    }
}