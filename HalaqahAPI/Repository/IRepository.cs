using System.Linq.Expressions;

namespace HalaqahAPI.Repository;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
    T? GetById(object id);
    void Insert(T obj);
    void Update(T obj);
    void Delete(object id);
    void Delete(T obj);
    void Save();
}