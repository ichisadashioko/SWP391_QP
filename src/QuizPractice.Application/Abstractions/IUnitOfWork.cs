namespace QuizPractice.Application.Abstractions;

/// <summary>
/// Commits everything the repositories staged, in one transaction. A service calls it
/// once per use case. AppDbContext implements it, so Application never references
/// Infrastructure (SDS II, design note of every sequence diagram).
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
