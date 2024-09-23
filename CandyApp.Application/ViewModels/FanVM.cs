namespace CandyApp.Application.ViewModels;

public sealed class FanVM {
    public int FanId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsTrash { get; set; }
}
