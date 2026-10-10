namespace QuizPractice.Domain.Entities;

public class AnswerOption
{
    public int Id { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    /// <summary>Exactly one option of a question is correct.</summary>
    public bool IsCorrect { get; set; }

    /// <summary>1 to 6.</summary>
    public byte DisplayOrder { get; set; }
}
