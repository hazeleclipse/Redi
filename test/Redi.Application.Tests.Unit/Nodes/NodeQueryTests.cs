using Moq;
using Redi.Application.Nodes.Queries.GetById;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;

namespace Redi.Application.Tests.Unit;

public class NodeQueryTests
{
    [Fact]
    public void GetById_INodeRepositoryIsNull_ThrowArgumentNullException()
    {
        // Arrange
        var id = Guid.NewGuid();
        INodeRepository? mockRepository = null;
        var getNodeById = new GetNodeById(id);

        // Act & Assert
        #pragma warning disable CS8604 // Intended to be null
        Assert.Throws<ArgumentNullException>(() => new GetNodeByIdHandler(mockRepository));
        #pragma warning restore CS8604
    }
    
    [Fact]
    public async void GetById_EntityExists_ReturnSpecifiedNode()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mockRepository = new Mock<INodeRepository>();
        mockRepository
            .Setup(r => r.GetById(id))
            .Returns( CoreNode.Create(id, "Node from Moq", null) );
        var getNodeById = new GetNodeById(id);
        var handler = new GetNodeByIdHandler(mockRepository.Object);

        // Act
        var nodeDto = await handler.Handle(getNodeById, new CancellationToken());

        // Assert
        Assert.NotNull(nodeDto);
        Assert.Equal(id, nodeDto.Id);
    }

    [Fact]
    public async void GetById_EntityDoesNotExist_ReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();
        var mockRepository = new Mock<INodeRepository>();
        mockRepository
            .Setup(r => r.GetById(id))
            .Returns( (Node?) null );
        var getNodeById = new GetNodeById(id);
        var handler = new GetNodeByIdHandler(mockRepository.Object);

        // Act
        var nodeDto = await handler.Handle(getNodeById, new CancellationToken());

        // Assert
        Assert.Null(nodeDto);
    }
}