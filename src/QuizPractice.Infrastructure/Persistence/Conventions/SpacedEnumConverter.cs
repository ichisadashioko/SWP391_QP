using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace QuizPractice.Infrastructure.Persistence.Conventions;

/// <summary>
/// Stores an enum as the words of its member name: InProgress ↔ "In Progress",
/// UserRole ↔ "User Role". These are the values BR-23 and D24 list, so the
/// database stays readable and the CHECK constraints quote the SRS wording.
/// </summary>
public sealed partial class SpacedEnumConverter<TEnum>() : ValueConverter<TEnum, string>(
    value => ToDatabase(value),
    text => FromDatabase(text))
    where TEnum : struct, Enum
{
    public static string ToDatabase(TEnum value) => WordStart().Replace(value.ToString(), " $1");

    public static TEnum FromDatabase(string text) => Enum.Parse<TEnum>(text.Replace(" ", string.Empty));

    /// <summary>The allowed values as a SQL list: 'In Progress', 'Submitted'.</summary>
    public static string SqlValueList() =>
        string.Join(", ", Enum.GetValues<TEnum>().Select(v => $"'{ToDatabase(v)}'"));

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex WordStart();
}
