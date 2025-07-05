using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Mssql.Adapter;
using Xunit;

namespace Postgre.Adapter.UnitTests;

public class RepositoryTests
{
    public class FakeEntity : Entity, IId
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }

    private static Mock<DbSet<FakeEntity>> CreateMockDbSet(List<FakeEntity> data)
    {
        var queryable = data.AsQueryable();

        var mockSet = new Mock<DbSet<FakeEntity>>();
        mockSet.As<IQueryable<FakeEntity>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockSet.As<IQueryable<FakeEntity>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockSet.As<IQueryable<FakeEntity>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockSet.As<IQueryable<FakeEntity>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

        return mockSet;
    }

    [Fact]
    public void Get_WithValidExpression_ShouldReturnFilteredResult()
    {
        var data = new List<FakeEntity>
        {
            new FakeEntity { Id = Guid.NewGuid() },
            new FakeEntity { Id = Guid.NewGuid() }
        };

        var mockSet = CreateMockDbSet(data);

        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.Set<FakeEntity>()).Returns(mockSet.Object);

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        var result = repository.Get(x => true).ToList();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task InsertAsync_WithValidEntity_ShouldAdd()
    {
        var mockSet = new Mock<DbSet<FakeEntity>>();
        var mockContext = new Mock<DbContext>();

        mockContext.Setup(c => c.Set<FakeEntity>()).Returns(mockSet.Object);

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        var entity = new FakeEntity();

        var result = await repository.InsertAsync(entity);

        Assert.Equal(entity, result);
        mockSet.Verify(s => s.AddAsync(entity, default), Times.Once);
    }

    [Fact]
    public void Update_WithValidEntity_ShouldCallUpdate()
    {
        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.Update(It.IsAny<FakeEntity>()));

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        var entity = new FakeEntity();

        var result = repository.Update(entity);

        Assert.Equal(entity, result);
        mockContext.Verify(c => c.Update(entity), Times.Once);
    }

    [Fact]
    public async Task SaveChangesAsync_ShouldCallContextSaveChanges()
    {
        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        var result = await repository.SaveChangesAsync();

        Assert.Equal(1, result);
        mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task AddRangeAsync_ShouldAddMultipleEntities()
    {
        var entities = new List<FakeEntity> { new(), new() };

        var mockSet = new Mock<DbSet<FakeEntity>>();
        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.Set<FakeEntity>()).Returns(mockSet.Object);

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        await repository.AddRangeAsync(entities);

        mockSet.Verify(s => s.AddRangeAsync(entities, default), Times.Once);
    }

    [Fact]
    public void UpdateRange_ShouldUpdateMultipleEntities()
    {
        var entities = new List<FakeEntity> { new(), new() };

        var mockSet = new Mock<DbSet<FakeEntity>>();
        var mockContext = new Mock<DbContext>();
        mockContext.Setup(c => c.Set<FakeEntity>()).Returns(mockSet.Object);

        var repository = new Repository<FakeEntity, DbContext>(mockContext.Object);

        repository.UpdateRange(entities);

        mockSet.Verify(s => s.UpdateRange(entities), Times.Once);
    }

   
    
}