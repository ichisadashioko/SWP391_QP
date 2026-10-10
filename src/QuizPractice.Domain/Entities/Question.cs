using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class Question
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>Topic the question belongs to.</summary>
    public int? LessonId { get; set; }
    public Lesson? Lesson { get; set; }

    /// <summary>Setting of type Question Level.</summary>
    public int LevelId { get; set; }
    public Setting Level { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    /// <summary>Relative path under App_Data/question-media, or a video link.</summary>
    public string? Media { get; set; }

    public string? Explanation { get; set; }

    /// <summary>An imported question starts Hidden until the Expert checks it (UC-55).</summary>
    public VisibilityStatus Status { get; set; } = VisibilityStatus.Shown;

    public List<AnswerOption> Options { get; set; } = [];
    public List<QuestionDimension> Dimensions { get; set; } = [];
}
