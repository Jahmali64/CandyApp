using Microsoft.EntityFrameworkCore;

namespace CandyApp.Infrastructure.Data;

public interface ICandyContext : IAsyncDisposable {
    DbSet<Fan> Fans { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}

public partial class CandyContext : DbContext, ICandyContext {

}
