using CandyApp.Application.ViewModels;
using CandyApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CandyApp.Application.Services;

public interface IAuthService {
    Task<AuthorizedUserVM> Login(UserLoginVM user);
    Task<AuthorizedUserVM> CreateAccount(UserRegistrationVM user);
}

public sealed class AuthService : IAuthService {
    private readonly IPirateKingDbContextFactory _contextFactory;
    private readonly CancellationToken _cancellationToken;

    public AuthService(IPirateKingDbContextFactory contextFactory, CancellationToken cancellationToken) {
        _contextFactory = contextFactory;
        _cancellationToken = cancellationToken;
    }

    public async Task<AuthorizedUserVM> Login(UserLoginVM user) {
        await using var context = await _contextFactory.CreateDbContextAsync();

        string email = user.Email.ToLower().Trim();
        var userAdmin = await context.UserAdministrators
            .Where(x => x.Email!.ToLower() == email && x.IsActive && !x.IsTrash)
            .Select(x => new {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Password = x.Password,
            }).FirstOrDefaultAsync(_cancellationToken);

        if (userAdmin is null) throw new InvalidOperationException("Email not found.");
        if (userAdmin.Password != user.Password) throw new InvalidOperationException("Password does not match.");

        return new AuthorizedUserVM {
            FirstName = userAdmin.FirstName ?? string.Empty,
            LastName= userAdmin.LastName ?? string.Empty,
            Email = email,
        };
    }

    public async Task<AuthorizedUserVM> CreateAccount(UserRegistrationVM user) {
        await using var context = await _contextFactory.CreateDbContextAsync();

        string email = user.Email.ToLower().Trim();
        var userAdministratorID = await context.UserAdministrators
            .Where(x => x.Email!.ToLower() == email)
            .Select(x => x.UserAdministratorId).FirstOrDefaultAsync(_cancellationToken);

        if (userAdministratorID > 0) throw new InvalidOperationException("Email already exist.");

        var userAdmin = new UserAdministrator {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = email,
            Password = user.Password,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsTrash = false,
        };
        await context.UserAdministrators.AddAsync(userAdmin, _cancellationToken);
        await context.SaveChangesAsync();

        return new AuthorizedUserVM {
            FirstName = userAdmin.FirstName ?? string.Empty,
            LastName= userAdmin.LastName ?? string.Empty,
            Email = email,
        };
    }
}
