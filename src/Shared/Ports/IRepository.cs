using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Ports;

public interface IRepository<T,TContext>  
        where T : Entity ,IId
        where TContext : DbContext
{
    
    public IQueryable<T> GetPaging(int skip, int take, Expression<Func<T, bool>> expression);
    
    public IQueryable<T> Get(Expression<Func<T, bool>> expression);

    public Task<T> InsertAsync(T entity);

    public Task<T> UpdateAsync(T entity, Expression<Func<T, bool>> expression);
    
    
    



}