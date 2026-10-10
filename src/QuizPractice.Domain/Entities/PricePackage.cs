using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class PricePackage
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    /// <summary>1 to 24 months; there is no lifetime package (D11).</summary>
    public byte AccessMonths { get; set; }

    /// <summary>Whole VND.</summary>
    public decimal ListPrice { get; set; }

    /// <summary>Whole VND, not above the list price.</summary>
    public decimal SalePrice { get; set; }

    public string? Description { get; set; }
    public ActiveStatus Status { get; set; } = ActiveStatus.Active;
}
