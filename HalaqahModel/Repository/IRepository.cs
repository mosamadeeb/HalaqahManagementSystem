namespace HalaqahModel.Repository;

public interface IRepository<T> where T : class
{
    IQueryable<T> GetAll();
    IQueryable<T> GetAllThenInclude(params string[] navigationsToInclude);
    T? GetById(object id);
    T? GetByIdThenInclude(object id, params string[] navigationsToInclude);
    void Insert(T obj);
    void Update(T obj);
    void Delete(object id);
    void Delete(T obj);
}