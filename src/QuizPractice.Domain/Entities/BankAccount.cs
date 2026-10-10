namespace QuizPractice.Domain.Entities;

/// <summary>The one receiving account (BR-24). The row is seeded and only updated (UC-70).</summary>
public class BankAccount
{
    public const int SingleId = 1;

    public int Id { get; set; } = SingleId;
    public string BankName { get; set; } = string.Empty;

    /// <summary>6-digit bank code of the VietQR list.</summary>
    public string BankBin { get; set; } = string.Empty;

    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>Holder name in capitals without accents.</summary>
    public string AccountName { get; set; } = string.Empty;

    /// <summary>Concurrency token for UC-70 E2.</summary>
    public DateTime UpdatedAt { get; set; }
}
