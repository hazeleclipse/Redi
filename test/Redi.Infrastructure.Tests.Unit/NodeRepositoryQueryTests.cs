using Microsoft.EntityFrameworkCore;
using Moq;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Infrastructure.Persistence.EfCore;
using Redi.Infrastructure.Persistence.EfCore.Repositories;

namespace Redi.Infrastructure.Tests.Unit;

public class NodeRepositoryQueryTests
{

    private readonly INodeRepository _repo;

    NodeId idExists = Guid.NewGuid();
    NodeId idCore = Guid.NewGuid();
    NodeId idByWeight = Guid.NewGuid();
    NodeId idDoesNotExist = Guid.NewGuid();


    public NodeRepositoryQueryTests()
    {
        var data = new List<Node>()
        {
            CoreNode.Create(idCore, "Core Node", null),
            ByWeightNode.Create(idByWeight, "ByWeight Node", null),
            CoreNode.Create(idExists, "Core Node target", null),
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Node>>();
        mockSet.As<IQueryable<Node>>().Setup(s => s.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Node>>().Setup(s => s.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Node>>().Setup(s => s.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Node>>().Setup(s => s.GetEnumerator()).Returns(() => data.GetEnumerator());
        mockSet
            .Setup(s => s.Find(It.IsAny<object[]>()))
            .Returns<object[]>(d => data.FirstOrDefault(x => x.Id == (NodeId)d[0]));

        var mockContext = new Mock<RediDbContext>();
        mockContext.Setup(c => c.Nodes).Returns(mockSet.Object);

        _repo = new NodeRepository(mockContext.Object);
    }

    [Fact]
    public void GetById_EntityExists_ReturnEntity()
    {
        // Arrange
        // Act
        var node = _repo.GetById(idExists);

        // Assert        
        Assert.NotNull(node);
        Assert.Equal(idExists, node.Id);
    }

    [Fact]
    public void GetById_EntityDoesNotExist_ReturnNull()
    {
        
        // Arrange
        // Act
        var node = _repo.GetById(idDoesNotExist);

        // Assert        
        Assert.Null(node);
    }
}