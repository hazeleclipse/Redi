using System.Diagnostics.CodeAnalysis;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Domain.Tests.Unit;

[ExcludeFromCodeCoverage(Justification = "Test Code")]
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

    [Fact]
    public void Update_AnyNullArgument_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var targetNode = CoreNode.Create(id, "target", null);
        

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => targetNode.Update(null!));
    }
}
