using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuizPractice.Infrastructure.Persistence.Conventions;

/// <summary>
/// Column names of the SRS: snake_case of the property (PassRate → pass_rate), and
/// the key Id → user_id, slider_id... A configuration that sets a column name itself
/// (RoleId → role) wins over this convention.
/// </summary>
public static partial class NamingConvention
{
    public static string ToSnakeCase(string name) => WordStart().Replace(name, "_$1").ToLowerInvariant();

    public static void Apply(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                if (property.FindAnnotation(RelationalAnnotationNames.ColumnName) is not null)
                {
                    continue;
                }

                var column = property.Name == "Id"
                    ? ToSnakeCase(entity.ClrType.Name) + "_id"
                    : ToSnakeCase(property.Name);
                property.SetColumnName(column);
            }
        }
    }

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex WordStart();
}
