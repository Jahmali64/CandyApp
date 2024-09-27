namespace CandyApp.UI.Models;

public record class UserLoginViewModel {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}