using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using Mssql.Adapter;
using Xunit;

namespace Postgre.Adapter.UnitTests;

public class UnitOfWorkTests
{
    [Fact]
        public void Constructor_WithDbContext_ShouldSetContext()
        {
            var mockDbContext = new Mock<DbContext>();
            var uow = new UnitOfWork<DbContext>(mockDbContext.Object);
            Assert.NotNull(uow);
        }

        [Fact]
        public void BeginTransactionAsync_ShouldStartTransaction()
        {
            // Arrange
            var mockTransaction = new Mock<IDbContextTransaction>();
            var mockDatabase = new Mock<DatabaseFacade>(new Mock<DbContext>().Object);
            mockDatabase.Setup(db => db.BeginTransaction()).Returns(mockTransaction.Object);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);

            var uow = new UnitOfWork<DbContext>(mockContext.Object);

            // Act
            uow.BeginTransactionAsync();

            // Assert (indirect verification)
            mockDatabase.Verify(db => db.BeginTransaction(), Times.Once);
        }

        [Fact]
        public async Task CommitAsync_ShouldCallSaveChangesAndCommit()
        {
            // Arrange
            var mockTransaction = new Mock<IDbContextTransaction>();
            mockTransaction.Setup(t => t.CommitAsync(default)).Returns(Task.CompletedTask);

            var mockDatabase = new Mock<DatabaseFacade>(new Mock<DbContext>().Object);
            mockDatabase.Setup(db => db.BeginTransaction()).Returns(mockTransaction.Object);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);
            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);

            var uow = new UnitOfWork<DbContext>(mockContext.Object);
            uow.BeginTransactionAsync();

            // Act
            var result = await uow.CommitAsync();

            // Assert
            Assert.Equal(1, result);
            mockTransaction.Verify(t => t.CommitAsync(default), Times.Once);
        }

        [Fact]
        public async Task CommitAsync_WithoutBeginTransaction_ShouldThrow()
        {
            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var uow = new UnitOfWork<DbContext>(mockContext.Object);

            await Assert.ThrowsAsync<NullReferenceException>(() => uow.CommitAsync());
        }

        [Fact]
        public async Task RollBackAsync_WithTransaction_ShouldRollback()
        {
            var mockTransaction = new Mock<IDbContextTransaction>();
            mockTransaction.Setup(t => t.RollbackAsync(default)).Returns(Task.CompletedTask);

            var mockDatabase = new Mock<DatabaseFacade>(new Mock<DbContext>().Object);
            mockDatabase.Setup(db => db.BeginTransaction()).Returns(mockTransaction.Object);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);

            var uow = new UnitOfWork<DbContext>(mockContext.Object);
            uow.BeginTransactionAsync();

            await uow.RollBackAsync();

            mockTransaction.Verify(t => t.RollbackAsync(default), Times.Once);
        }

        [Fact]
        public async Task RollBackAsync_WithoutTransaction_ShouldNotThrow()
        {
            var mockContext = new Mock<DbContext>();
            var uow = new UnitOfWork<DbContext>(mockContext.Object);

            var ex = await Record.ExceptionAsync(() => uow.RollBackAsync());
            Assert.Null(ex);
        }

        [Fact]
        public void Dispose_ShouldDisposeContextAndTransaction()
        {
            var mockTransaction = new Mock<IDbContextTransaction>();
            var mockDatabase = new Mock<DatabaseFacade>(new Mock<DbContext>().Object);
            mockDatabase.Setup(db => db.BeginTransaction()).Returns(mockTransaction.Object);

            var mockContext = new Mock<DbContext>();
            mockContext.Setup(c => c.Database).Returns(mockDatabase.Object);
            mockContext.Setup(c => c.Dispose());

            var uow = new UnitOfWork<DbContext>(mockContext.Object);
            uow.BeginTransactionAsync();

            uow.Dispose();

            mockTransaction.Verify(t => t.Dispose(), Times.Once);
            mockContext.Verify(c => c.Dispose(), Times.Once);
        }
}
