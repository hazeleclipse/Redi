using System.Diagnostics.CodeAnalysis;
using Moq;
using Redi.Application.Nodes.Commands.Update;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Tests.Unit;

[ExcludeFromCodeCoverage(Justification = "Test Code")]
public class NodeCommandTests
{
    [Fact]
    public async void UpdateNode_EntityExists_ChangesPersisted()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sourceNode = CoreNode.Create(id, "source", null);
        var targetNode = CoreNode.Create(id, "target", null);

        var mockRepository = new Mock<INodeRepository>();
        mockRepository
            .Setup(r => r.GetById(id))
            .Returns(targetNode);

        mockRepository
            .Setup(r => r.Update(targetNode))
            .Verifiable();

        var updateNode = new UpdateNode(id, sourceNode.Name);
        var handler = new UpdateNodeHandler(mockRepository.Object);

        // Act
        await handler.Handle(updateNode, new CancellationToken());

        // Assert
        Assert.Equal(sourceNode.Name, targetNode.Name);
        mockRepository.Verify(r => r.Update(targetNode), Times.Once());
    }

    [Fact]
    public async void UpdateNode_EntityDoesNotExist_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var coreNode = CoreNode.Create(id, "target", null);

        var mockRepository = new Mock<INodeRepository>();
        mockRepository
            .Setup(r => r.GetById(id))
            .Returns((Node?) null);

        var updateNode = new UpdateNode(id, coreNode.Name);
        var handler = new UpdateNodeHandler(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await handler.Handle(updateNode, new CancellationToken()));
    }
}