using URLShortener.Api.Persistence;

namespace URLShortener.Api.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IShortLinkRepository shortLinks;
    public IShortLinkRepository ShortLinks => shortLinks = new ShortLinkRepository(context);

    public IShortLinkRepository ShortLinkRepository { get; }
    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

    public void Dispose() => context.Dispose();
}