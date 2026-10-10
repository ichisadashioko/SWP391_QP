namespace QuizPractice.Application.Common;

/// <summary>
/// An uploaded file as the application sees it, without ASP.NET types. The controller
/// builds it from IFormFile. Content must be seekable: the validator reads the first
/// bytes to check the real type, then the storage reads it again from the start.
/// </summary>
public sealed record FileUpload(Stream Content, string FileName, long Length)
{
    public string Extension => Path.GetExtension(FileName).ToLowerInvariant();
}
