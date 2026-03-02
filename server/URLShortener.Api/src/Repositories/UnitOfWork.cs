using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUrlRepository urls;
    public IUrlRepository Urls => urls ??= new UrlRepository(context);
    
    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public void Dispose() => context.Dispose();
}
