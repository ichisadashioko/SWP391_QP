using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>Filled only for a lesson of type Quiz.</summary>
    public int? QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    /// <summary>Setting of type Lesson Type.</summary>
    public int TypeId { get; set; }
    public Setting Type { get; set; } = null!;

    /// <summary>Subject Topic the lesson sits under; empty for a topic (D12).</summary>
    public int? TopicId { get; set; }
    public Lesson? Topic { get; set; }

    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public string? VideoUrl { get; set; }
    public string? HtmlContent { get; set; }
    public ActiveStatus Status { get; set; } = ActiveStatus.Active;
}
