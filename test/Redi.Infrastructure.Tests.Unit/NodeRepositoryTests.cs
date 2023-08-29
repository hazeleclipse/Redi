using Microsoft.EntityFrameworkCore;
using Moq;
using Redi.Domain.Aggregates.NodeAggregate;
using Redi.Domain.Aggregates.NodeAggregate.ValueObjects;
using Redi.Infrastructure.Persistence.EfCore;
using Redi.Infrastructure.Persistence.EfCore.Repositories;

namespace Redi.Infrastructure.Tests.Unit;

public class NodeRepositoryTests
{
    [Fact]
    public void GetById_EntityExists_ReturnEntity()
    {
        // Arrange
        var id = Guid.NewGuid();

        var data = new List<Node>()
        {
            CoreNode.Create(Guid.NewGuid(), "From Moq 1", null),
            CoreNode.Create(Guid.NewGuid(), "From Moq 2", null),
            CoreNode.Create(id, "target", null),
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Node>>();
        mockSet.As<IQueryable<Node>>().Setup(s => s.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Node>>().Setup(s => s.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Node>>().Setup(s => s.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Node>>().Setup(s => s.GetEnumerator()).Returns(() => data.GetEnumerator());
        mockSet
            .Setup(s => s.Find(It.IsAny<object[]>()))
            .Returns(data.FirstOrDefault(x => x.Id == id));

        var mockContext = new Mock<RediDbContext>();
        mockContext.Setup(c => c.Nodes).Returns(mockSet.Object);

        var repo = new NodeRepository(mockContext.Object);

        // Act
        var node = repo.GetById(id);

        // Assert        
        Assert.NotNull(node);
        Assert.Equal(id, (Guid)node.Id);

    }
}