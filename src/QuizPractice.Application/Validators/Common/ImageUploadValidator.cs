using FluentValidation;
using QuizPractice.Application.Common;
using QuizPractice.Domain.Constants;

namespace QuizPractice.Application.Validators.Common;

/// <summary>
/// Image upload rule of BR-17: JPG or PNG, up to 2 MB. The extension alone proves
/// nothing, so the first bytes must also be a JPG or PNG signature; an SVG or HTML
/// file renamed to .png is refused (stored XSS).
/// </summary>
public class ImageUploadValidator : AbstractValidator<FileUpload>
{
    private static readonly string[] Extensions = [".jpg", ".jpeg", ".png"];
    private static readonly byte[] Png = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
    private static readonly byte[] Jpg = [0xFF, 0xD8, 0xFF];

    public ImageUploadValidator()
    {
        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("The image is empty.")
            .LessThanOrEqualTo(FieldLength.ImageMaxBytes).WithMessage("The image must be 2 MB or smaller.");

        RuleFor(x => x.Extension)
            .Must(e => Extensions.Contains(e)).WithMessage("The image must be a JPG or PNG file.");

        RuleFor(x => x.Content)
            .Must(HasImageSignature).WithMessage("The file is not a real JPG or PNG image.")
            .When(x => Extensions.Contains(x.Extension));
    }

    private static bool HasImageSignature(Stream content)
    {
        Span<byte> head = stackalloc byte[8];
        content.Position = 0;
        var read = content.ReadAtLeast(head, head.Length, throwOnEndOfStream: false);
        content.Position = 0;
        return head[..read].StartsWith(Png) || head[..read].StartsWith(Jpg);
    }
}
