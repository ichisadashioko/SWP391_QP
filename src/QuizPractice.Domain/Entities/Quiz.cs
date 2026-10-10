using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

public class Quiz
{
    public int Id { get; set; }

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>Setting of type Test Type: Simulation or Lesson quiz.</summary>
    public int TypeId { get; set; }
    public Setting Type { get; set; } = null!;

    /// <summary>Setting of type Question Level.</summary>
    public int LevelId { get; set; }
    public Setting Level { get; set; } = null!;

    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    /// <summary>Equals the sum of its rules (UC-57).</summary>
    public short QuestionCount { get; set; }

    /// <summary>"Topic", or the value of the Dimension Type setting the questions are drawn by.</summary>
    public string SelectionType { get; set; } = string.Empty;

    public short DurationMin { get; set; }

    /// <summary>Starts from the subject pass rate (BR-21).</summary>
    public byte PassRate { get; set; }

    public ActiveStatus Status { get; set; } = ActiveStatus.Active;

    public List<QuizRule> Rules { get; set; } = [];

    /// <summary>Fixed set of a lesson quiz, drawn when it is saved (D13).</summary>
    public List<QuizQuestion> Questions { get; set; } = [];
}
