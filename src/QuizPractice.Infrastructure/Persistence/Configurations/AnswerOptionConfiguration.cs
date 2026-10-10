using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
{
    public void Configure(EntityTypeBuilder<AnswerOption> b)
    {
        b.ToTable("AnswerOptions");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("option_id");
        b.Property(x => x.Content).HasMaxLength(FieldLength.AnswerOption);
        b.Property(x => x.IsCorrect).HasDbDefault(false);
        b.HasCheck("display_order", "[display_order] BETWEEN 1 AND 6");

        // At most one correct option per question; the validator checks "at least one".
        b.HasIndex([nameof(AnswerOption.QuestionId)], "UX_AnswerOptions_question_id_correct")
            .IsUnique()
            .HasFilter("[is_correct] = 1");

        // The filtered index above only holds correct options, so loading all the
        // options of a question needs this one too (EF would otherwise skip it).
        b.HasIndex([nameof(AnswerOption.QuestionId)], "IX_AnswerOptions_question_id");
    }
}
