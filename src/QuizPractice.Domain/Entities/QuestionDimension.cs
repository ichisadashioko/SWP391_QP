namespace QuizPractice.Domain.Entities;

/// <summary>Links a question to the dimensions it tests; blocks the delete of a used dimension (D10).</summary>
public class QuestionDimension
{
    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public int DimensionId { get; set; }
    public SubjectDimension Dimension { get; set; } = null!;
}
