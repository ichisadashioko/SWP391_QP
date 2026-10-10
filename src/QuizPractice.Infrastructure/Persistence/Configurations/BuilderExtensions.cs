using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Infrastructure.Persistence.Conventions;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

/// <summary>Short forms for the column rules that repeat across the 19 tables of SDS I.3.</summary>
internal static class BuilderExtensions
{
    /// <summary>CHECK constraint named CK_{table}_{name}. Call ToTable first.</summary>
    public static EntityTypeBuilder<T> HasCheck<T>(this EntityTypeBuilder<T> builder, string name, string sql)
        where T : class
    {
        var table = builder.Metadata.GetTableName();
        builder.ToTable(t => t.HasCheckConstraint($"CK_{table}_{name}", sql));
        return builder;
    }

    /// <summary>
    /// varchar column holding the words of the enum (BR-23 values), with a CHECK on the
    /// allowed values and, when given, a DEFAULT. EF always sends the value it has, so
    /// the C# initializer of the entity decides the default of a new row; the DEFAULT
    /// only serves rows written by hand in SQL.
    /// </summary>
    public static PropertyBuilder<TEnum> HasEnumColumn<T, TEnum>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, TEnum>> property, int maxLength, TEnum? dbDefault = null)
        where T : class
        where TEnum : struct, Enum
    {
        var column = AddEnumCheck<T, TEnum>(builder, property, maxLength);
        var prop = builder.Property(property)
            .HasColumnName(column)
            .HasConversion(new SpacedEnumConverter<TEnum>())
            .HasMaxLength(maxLength)
            .IsUnicode(false);
        if (dbDefault is { } value)
        {
            prop.HasDefaultValue(value).ValueGeneratedNever();
        }

        return prop;
    }

    /// <summary>Same as above for a nullable enum column; NULL passes the CHECK.</summary>
    public static PropertyBuilder<TEnum?> HasEnumColumn<T, TEnum>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, TEnum?>> property, int maxLength)
        where T : class
        where TEnum : struct, Enum
    {
        var column = AddEnumCheck<T, TEnum>(builder, property, maxLength);
        return builder.Property(property)
            .HasColumnName(column)
            .HasConversion(new SpacedEnumConverter<TEnum>())
            .HasMaxLength(maxLength)
            .IsUnicode(false);
    }

    /// <summary>varchar / char column (no Unicode): codes, digits, hashes.</summary>
    public static PropertyBuilder<TText> IsAscii<TText>(this PropertyBuilder<TText> property, int length, bool fixedLength = false) =>
        property.HasMaxLength(length).IsUnicode(false).IsFixedLength(fixedLength);

    /// <summary>
    /// DEFAULT for a column EF still always writes (see HasEnumColumn). Without
    /// ValueGeneratedNever, EF would skip a value equal to the CLR default (false, 0)
    /// and the database would put its own default in its place.
    /// </summary>
    public static PropertyBuilder<TValue> HasDbDefault<TValue>(this PropertyBuilder<TValue> property, TValue value) =>
        property.HasDefaultValue(value).ValueGeneratedNever();

    /// <summary>datetime2(0) filled by the database with the UTC time when the row is inserted.</summary>
    public static PropertyBuilder<DateTime> HasUtcNowDefault(this PropertyBuilder<DateTime> property) =>
        property.HasDefaultValueSql("SYSUTCDATETIME()");

    private static string AddEnumCheck<T, TEnum>(EntityTypeBuilder<T> builder, LambdaExpression property, int maxLength)
        where T : class
        where TEnum : struct, Enum
    {
        var member = ((MemberExpression)property.Body).Member.Name;
        var column = NamingConvention.ToSnakeCase(member);
        var longest = Enum.GetValues<TEnum>().Max(v => SpacedEnumConverter<TEnum>.ToDatabase(v).Length);
        if (longest > maxLength)
        {
            throw new InvalidOperationException($"{typeof(T).Name}.{member}: a value has {longest} characters, the column {maxLength}.");
        }

        builder.HasCheck(column, $"[{column}] IN ({SpacedEnumConverter<TEnum>.SqlValueList()})");
        return column;
    }
}
