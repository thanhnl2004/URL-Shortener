using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class BaseRepository<T>(AppDbContext dbContext) : IBaseRepository<T> where T : class
{
    public void Add(T entity)
    {
       dbContext.Set<T>().Add(entity);
    }

    public void Remove(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }
}