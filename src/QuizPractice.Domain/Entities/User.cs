using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class User
{
    public int Id { get; set; }

    /// <summary>Login email; cannot be changed (BR-04).</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Empty until a self-registered account is verified (OI-16).</summary>
    public string? PasswordHash { get; set; }

    public string FullName { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Mobile { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Avatar { get; set; }

    /// <summary>Setting of type User Role.</summary>
    public int RoleId { get; set; }
    public Setting Role { get; set; } = null!;

    public UserStatus Status { get; set; } = UserStatus.Unverified;
    public DateTime CreatedAt { get; set; }
}
