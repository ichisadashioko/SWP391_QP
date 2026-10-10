namespace QuizPractice.Domain.Entities;

public class AttemptQuestion
{
    public int AttemptId { get; set; }
    public Attempt Attempt { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    /// <summary>Answer chosen; empty = unanswered, which scores as wrong (BR-21).</summary>
    public int? ChosenOptionId { get; set; }
    public AnswerOption? ChosenOption { get; set; }

    public short DisplayOrder { get; set; }

    /// <summary>Marked for review (UC-26).</summary>
    public bool IsMarked { get; set; }
}
