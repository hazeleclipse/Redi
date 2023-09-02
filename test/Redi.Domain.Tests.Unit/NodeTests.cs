using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Domain.Tests.Unit;

public class NodeTests
{

    [Fact]
    public void Update_ChangesAreMade_ChangesArePersisted()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sourceNode = CoreNode.Create(id, "source", null);
        var targetNode = CoreNode.Create(id, "target", null);
        
        // Act
        targetNode.Update(sourceNode.Name);

        // Assert
        Assert.Equal(sourceNode.Name, targetNode.Name);
    }
}
