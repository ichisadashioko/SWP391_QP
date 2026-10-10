namespace QuizPractice.Domain.Entities;

public class SubjectDimension
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>Setting of type Dimension Type (e.g. Domain, Group).</summary>
    public int TypeId { get; set; }
    public Setting Type { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
