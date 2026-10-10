using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class QuizQuestionConfiguration : IEntityTypeConfiguration<QuizQuestion>
{
    public void Configure(EntityTypeBuilder<QuizQuestion> b)
    {
        b.ToTable("QuizQuestions");
        b.HasKey(x => new { x.QuizId, x.QuestionId });

        // Follows its quiz when an unused quiz is deleted (UC-58).
        b.HasOne(x => x.Quiz).WithMany(q => q.Questions).HasForeignKey(x => x.QuizId).OnDelete(DeleteBehavior.Cascade);

        b.HasCheck("display_order", "[display_order] >= 1");
    }
}
