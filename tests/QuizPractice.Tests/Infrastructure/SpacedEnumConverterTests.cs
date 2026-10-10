using QuizPractice.Domain.Enums;
using QuizPractice.Infrastructure.Persistence.Conventions;

namespace QuizPractice.Tests.Infrastructure;

public class SpacedEnumConverterTests
{
    [Theory]
    [InlineData(AttemptStatus.InProgress, "In Progress")]
    [InlineData(AttemptStatus.Submitted, "Submitted")]
    public void Attempt_status_is_stored_in_the_words_of_BR_23(AttemptStatus value, string stored)
    {
        Assert.Equal(stored, SpacedEnumConverter<AttemptStatus>.ToDatabase(value));
        Assert.Equal(value, SpacedEnumConverter<AttemptStatus>.FromDatabase(stored));
    }

    [Fact]
    public void Setting_types_are_the_8_values_of_D24()
    {
        Assert.Equal(
            "'User Role', 'System Menu', 'Post Category', 'Subject Category', 'Test Type', "
            + "'Question Level', 'Lesson Type', 'Dimension Type'",
            SpacedEnumConverter<SettingType>.SqlValueList());
    }

    [Theory]
    [InlineData("PassRate", "pass_rate")]
    [InlineData("HtmlContent", "html_content")]
    [InlineData("BankAccount", "bank_account")]
    public void Property_names_map_to_the_snake_case_columns_of_the_SRS(string property, string column) =>
        Assert.Equal(column, NamingConvention.ToSnakeCase(property));
}
