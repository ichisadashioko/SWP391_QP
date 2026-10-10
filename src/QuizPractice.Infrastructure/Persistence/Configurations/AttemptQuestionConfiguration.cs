using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class AttemptQuestionConfiguration : IEntityTypeConfiguration<AttemptQuestion>
{
    public void Configure(EntityTypeBuilder<AttemptQuestion> b)
    {
        b.ToTable("AttemptQuestions");
        b.HasKey(x => new { x.AttemptId, x.QuestionId });
        b.HasCheck("display_order", "[display_order] >= 1");
        b.Property(x => x.IsMarked).HasDbDefault(false);
    }
}
