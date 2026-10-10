using QuizPractice.Domain.Enums;

namespace QuizPractice.Domain.Entities;

/// <summary>One practice or one simulation exam taken by a customer.</summary>
public class Attempt
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;

    /// <summary>Simulation exam; empty for a practice.</summary>
    public int? QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public AttemptMode Mode { get; set; }

    /// <summary>Empty for an exam.</summary>
    public SelectionType? SelectionType { get; set; }

    /// <summary>Topic or dimension chosen; empty when all were chosen. No foreign key.</summary>
    public int? GroupId { get; set; }

    public short QuestionCount { get; set; }

    /// <summary>Question on screen, kept on reload (BR-08).</summary>
    public short CurrentOrder { get; set; } = 1;

    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

    /// <summary>Percent correct, set when scored.</summary>
    public decimal? Score { get; set; }

    /// <summary>Pass rate applied, stored with the result (BR-21).</summary>
    public byte? PassRate { get; set; }

    public AttemptStatus Status { get; set; } = AttemptStatus.InProgress;

    public List<AttemptQuestion> Questions { get; set; } = [];
}
