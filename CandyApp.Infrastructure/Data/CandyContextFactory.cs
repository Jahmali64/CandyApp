using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public interface ICandyContextFactory : IAsyncDisposable {
    Task<ICandyContext> CreateDbContextAsync();
}

public partial class CandyContextFactory : DbContext, ICandyContextFactory {
    private readonly DbContextOptions<CandyContext> _options;

    public CandyContextFactory(DbContextOptions<CandyContext> options) {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<ICandyContext> CreateDbContextAsync() {
        return await Task.FromResult(new CandyContext(_options));
    }
}
