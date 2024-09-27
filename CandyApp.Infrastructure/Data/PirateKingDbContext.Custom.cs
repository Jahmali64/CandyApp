using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public interface IPirateKingDbContext : IAsyncDisposable {

    DbSet<BuildVersion> BuildVersions { get; set; }

    DbSet<ErrorLog> ErrorLogs { get; set; }

    DbSet<Fan> Fans { get; set; }

    DbSet<UserAdministrator> UserAdministrators { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}

public partial class PirateKingDbContext : DbContext, IPirateKingDbContext {
}