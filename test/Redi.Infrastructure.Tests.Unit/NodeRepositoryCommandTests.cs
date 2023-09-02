using Microsoft.EntityFrameworkCore;
using Moq;
using Redi.Application.Persistence;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Infrastructure.Persistence.EfCore;
using Redi.Infrastructure.Persistence.EfCore.Repositories;

namespace Redi.Infrastructure.Tests.Unit;

public class NodeRepositoryCommandTests
{
    private readonly INodeRepository _repository;
    
    private readonly RediDbContext _context;

    NodeId idExists = Guid.NewGuid();
    NodeId idCore = Guid.NewGuid();
    NodeId idByWeight = Guid.NewGuid();
    NodeId idDoesNotExist = Guid.NewGuid();

    public NodeRepositoryCommandTests()
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
        _context = mockContext.Object;
        mockContext.Setup(c => c.Nodes).Returns(mockSet.Object);

        _repository = new NodeRepository(mockContext.Object);
    }

    [Fact]
    public void Update_EntityExists_ChangesPersisted()
    {
        // Arrange
        var node = _repository.GetById(idExists);        
        if (node is null) Assert.Fail($"{nameof(node) } is null.");
        node.Update(nameof(idExists));

        // Act
        _repository.Update(node);

        // Assert
        var data = _context.Nodes.Find(idExists);
        Assert.Equal(nameof(idExists), data?.Name);
    }
}
