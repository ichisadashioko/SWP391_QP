using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

/// <summary>Banner of the public home page (UC-38 to UC-40).</summary>
public class Slider
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    /// <summary>Relative path under /uploads/sliders.</summary>
    public string Image { get; set; } = string.Empty;

    public string? Backlink { get; set; }
    public int DisplayOrder { get; set; }
    public string? Notes { get; set; }
    public VisibilityStatus Status { get; set; } = VisibilityStatus.Hidden;
}
