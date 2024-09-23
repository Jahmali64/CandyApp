using System;
using System.Collections.Generic;

namespace CandyApp.Infrastructure.Data;

public partial class Fan
{
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
