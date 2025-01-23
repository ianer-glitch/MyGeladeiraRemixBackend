﻿using System.Linq.Expressions;
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
        return context.Set<T>().Where(expression);  
    }

    public async Task<T> InsertAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public T Update(T entity, Expression<Func<T, bool>> expression)
    {
        context.Update(entity);
        return entity;
        
    }
}