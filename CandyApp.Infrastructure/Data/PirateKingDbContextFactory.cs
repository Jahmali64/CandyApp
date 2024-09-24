using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public interface IPirateKingDbContextFactory : IAsyncDisposable {
    Task<IPirateKingDbContext> CreateDbContextAsync();
}

public partial class PirateKingDbContextFactory : DbContext, IPirateKingDbContextFactory {
    private readonly DbContextOptions<PirateKingDbContext> _options;

    public PirateKingDbContextFactory(DbContextOptions<PirateKingDbContext> options) {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IPirateKingDbContext> CreateDbContextAsync() {
        return await Task.FromResult(new PirateKingDbContext(_options));
    }
}