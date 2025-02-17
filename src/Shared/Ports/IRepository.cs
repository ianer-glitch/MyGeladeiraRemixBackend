﻿using System.Linq.Expressions;
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

    public T Update(T entity);

    public Task<int> SaveChangesAsync();

    public  Task AddRangeAsync(IEnumerable<T> entities);

    public void UpdateRange(IEnumerable<T> entities);






}