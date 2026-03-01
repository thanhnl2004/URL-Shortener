namespace URLShortener.Api.Repositories;

public interface IUnitOfWork : IDisposable
{
    IShortLinkRepository ShortLinks { get; }
    Task<int> SaveChangesAsync();
}