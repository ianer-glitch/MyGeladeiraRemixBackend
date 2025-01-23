using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;
using Ports;

namespace Postgre.Adapter;

public class Repository<T,TContext>(TContext context) : IRepository<T,TContext>
    where T : Entity ,IId
    where TContext : DbContext
{
    public IQueryable<T> GetPaging(int skip, int take, Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Get(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<T> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }
}