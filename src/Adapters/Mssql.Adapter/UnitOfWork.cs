using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mssql.Adapter;

public class UnitOfWork<TDbContext> where TDbContext : DbContext , IDisposable
{
    private readonly TDbContext _dbContext;
    
    private IDbContextTransaction?  Transaction { get; set; }
    
    public UnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void BeginTransactionAsync()
    {
        Transaction = _dbContext.Database.BeginTransaction(); 
    }

    public async Task<int> CommitAsync()
    {
        
        var result = await _dbContext.SaveChangesAsync();
        await Transaction!.CommitAsync() ;
        return result; 
    }

    public async Task RollBackAsync()
    {
        if (Transaction != null)
        {
            await Transaction.RollbackAsync();
        }
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        _dbContext?.Dispose();
    }
}