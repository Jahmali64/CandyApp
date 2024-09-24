using CandyApp.Application.ViewModels;
using CandyApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandyApp.Application.Services;

public interface IFanService {
    Task<List<FanVM>> GetFansAsync();
    Task<FanVM?> GetFanAsync(int fanId);
    Task SaveAsync(FanVM fanVM);
    Task DeleteAsync(int fanId);
}

public sealed class FanService : IFanService {
    private readonly IPirateKingDbContextFactory _contextFactory;
    private readonly CancellationToken _cancellationToken;

    public FanService(IPirateKingDbContextFactory contextFactory, CancellationToken cancellationToken) {
        _contextFactory = contextFactory;
        _cancellationToken = cancellationToken;
    }

    public async Task<List<FanVM>> GetFansAsync() {
        await using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Fans
            .Where(f => !f.IsTrash)
            .Select(f => new FanVM {
                FanId = f.FanId,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Email = f.Email,
                IsActive = f.IsActive,
            })
            .ToListAsync(_cancellationToken);
    }

    public async Task<FanVM?> GetFanAsync(int fanId) {
        await using var context = await _contextFactory.CreateDbContextAsync();

        return await context.Fans
            .Where(f => f.FanId == fanId && !f.IsTrash)
            .Select(f => new FanVM {
                FanId = f.FanId,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Email = f.Email,
                IsActive = f.IsActive,
            })
            .FirstOrDefaultAsync(_cancellationToken);
    }

    public async Task SaveAsync(FanVM fanVM) {
        await using var context = await _contextFactory.CreateDbContextAsync();

        async Task SaveNewFanAsync() {
            var fan = new Fan {
                FirstName = fanVM.FirstName,
                LastName = fanVM.LastName,
                Email = fanVM.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
                IsTrash = false
            };

            await context.Fans.AddAsync(fan, _cancellationToken);
            await context.SaveChangesAsync(_cancellationToken);
        }

        async Task UpdateFanAsync() {
            int rowsAffected = await context.Fans
                .Where(f => f.FanId == fanVM.FanId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(f => f.FirstName, fanVM.FirstName)
                    .SetProperty(f => f.LastName, fanVM.LastName)
                    .SetProperty(f => f.Email, fanVM.Email)
                    .SetProperty(f => f.UpdatedAt, DateTime.UtcNow)
                    , _cancellationToken
                );

            if (rowsAffected == 0) throw new InvalidOperationException("Fan not found.");
        }

        if (fanVM.FanId == 0) await SaveNewFanAsync();
        else await UpdateFanAsync();
    }

    public async Task DeleteAsync(int fanId) {
        await using var context = await _contextFactory.CreateDbContextAsync();

        int rowsAffected = await context.Fans
            .Where(f => f.FanId == fanId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(f => f.IsTrash, true)
                .SetProperty(f => f.DeletedAt, DateTime.UtcNow)
                , _cancellationToken
            );
    }
}
