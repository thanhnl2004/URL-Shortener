namespace URLShortener.Api.Repositories;

public interface IBaseRepository<T> where T : class
{
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}