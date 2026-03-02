namespace URLShortener.Api.Repositories;

public interface IUnitOfWork : IDisposable
{
    IUrlRepository Urls { get; }
    Task<int> SaveChangesAsync();
}
