using Moq;
using Redi.Application.Nodes.Commands.Update;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Tests.Unit;

public class NodeCommandTests
{
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

        var updateNode = new UpdateNode(coreNode);
        var handler = new UpdateNodeHandler(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await handler.Handle(updateNode, new CancellationToken()));
    }
}