using HalaqahModel.Context;
using Microsoft.EntityFrameworkCore;

namespace HalaqahModel.Repository;

public class GenericRepository<T>(HalaqahContext context) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public IQueryable<T> GetAll()
    {
        // Do not track changes. Changes should be made through the repository.
        return _dbSet.AsNoTracking();
    }

    public IQueryable<T> GetAllThenInclude(params string[] navigationsToInclude)
    {
        return navigationsToInclude.Length == 0 ? GetAll() : GetAll().Include(string.Join('.', navigationsToInclude));
    }

    public T? GetById(object id)
    {
        var obj = _dbSet.Find(id);
        if (obj == null)
        {
            return null;
        }

        // Detach the object from the context to make it read-only.
        context.Entry(obj).State = EntityState.Detached;
        return obj;
    }

    public T? GetByIdThenInclude(object id,  params string[] navigationsToInclude)
    {
        var obj = _dbSet.Find(id);
        if (obj == null)
        {
            return null;
        }

        var entry = context.Entry(obj);
        
        var toLoad = entry.Navigations.Where(n => navigationsToInclude.Contains(n.Metadata.Name)).ToList();
        foreach (var navigationEntry in toLoad)
        {
            navigationEntry.Load();
        }
        
        // Detach the object from the context to make it read-only.
        entry.State = EntityState.Detached;
        return obj;
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
}