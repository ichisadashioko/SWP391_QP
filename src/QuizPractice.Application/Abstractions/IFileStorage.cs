namespace QuizPractice.Application.Abstractions;

/// <summary>
/// Stores uploaded files on the server; the database keeps only the relative path
/// (SDS I.1). The file always gets a new random name, never the one the user sent.
/// </summary>
public interface IFileStorage
{
    /// <summary>Saves the content under /uploads/{folder}/ and returns its relative path.</summary>
    Task<string> SaveAsync(Stream content, string extension, string folder, CancellationToken ct = default);

    /// <summary>Removes a file saved earlier. A missing file is not an error.</summary>
    void Delete(string relativePath);
}
