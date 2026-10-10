using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class Registration
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    public int PackageId { get; set; }
    public PricePackage Package { get; set; } = null!;

    /// <summary>Customer; empty only until Paid creates the account (BR-15).</summary>
    public int? UserId { get; set; }
    public User? User { get; set; }

    /// <summary>Sale user who recorded it; empty for an online registration (D18).</summary>
    public int? CreatedById { get; set; }
    public User? CreatedBy { get; set; }

    /// <summary>Last user who changed it (NFR-10).</summary>
    public int UpdatedById { get; set; }
    public User UpdatedBy { get; set; } = null!;

    /// <summary>Invoice it is being paid in, if any (BR-24).</summary>
    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }

    // Contact snapshot taken at registration time.
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public Gender Gender { get; set; }

    /// <summary>Sale price at registration time, whole VND.</summary>
    public decimal TotalCost { get; set; }

    /// <summary>Concurrency token: two Sale users cannot both settle it (BR-22).</summary>
    public RegistrationStatus Status { get; set; } = RegistrationStatus.Submitted;

    public DateTime RegisteredAt { get; set; }

    /// <summary>The day it becomes Paid (BR-14).</summary>
    public DateOnly? ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    public string? Notes { get; set; }
}
