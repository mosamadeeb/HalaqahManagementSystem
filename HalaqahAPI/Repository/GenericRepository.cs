using System.Linq.Expressions;
using HalaqahAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace HalaqahAPI.Repository;

public class GenericRepository<T>(HalaqahContext context) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public IQueryable<T> GetAll()
    {
        // Do not track changes. Changes should be made through the repository.
        return _dbSet.AsNoTracking();
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.AsNoTracking().Where(predicate);
    }

    public T? GetById(object id)
    {
        return _dbSet.Find(id);
    }

    public void Insert(T obj)
    {
        _dbSet.Add(obj);
    }

    public void Update(T obj)
    {
        _dbSet.Attach(obj);
        context.Entry(obj).State = EntityState.Modified;
    }

    public void Delete(object id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }
    
    public void Delete(T obj)
    {
        if (context.Entry(obj).State == EntityState.Detached)
        {
            _dbSet.Attach(obj);
        }
        
        _dbSet.Remove(obj);
    }

    public void Save()
    {
        context.SaveChanges();
    }
}