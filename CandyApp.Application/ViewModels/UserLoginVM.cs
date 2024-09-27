namespace CandyApp.Application.ViewModels;

public record class UserLoginVM {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}