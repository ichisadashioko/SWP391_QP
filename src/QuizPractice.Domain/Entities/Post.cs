using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class Post
{
    public int Id { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; } = null!;

    /// <summary>Setting of type Post Category.</summary>
    public int CategoryId { get; set; }
    public Setting Category { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Thumbnail { get; set; } = string.Empty;
    public string Brief { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
    public VisibilityStatus Status { get; set; } = VisibilityStatus.Hidden;
    public DateTime UpdatedAt { get; set; }
}
