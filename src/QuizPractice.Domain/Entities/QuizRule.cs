namespace QuizPractice.Domain.Entities;

/// <summary>How many questions a quiz draws from one group (UC-57).</summary>
public class QuizRule
{
    public int QuizId { get; set; }
    public Quiz Quiz { get; set; } = null!;

    /// <summary>
    /// lesson_id of a topic or dimension_id, as Quiz.SelectionType says. No foreign
    /// key: the service checks it.
    /// </summary>
    public int GroupId { get; set; }

    public short QuestionCount { get; set; }
}
