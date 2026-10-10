using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class QuizRuleConfiguration : IEntityTypeConfiguration<QuizRule>
{
    public void Configure(EntityTypeBuilder<QuizRule> b)
    {
        b.ToTable("QuizRules");
        b.HasKey(x => new { x.QuizId, x.GroupId });

        // Follows its quiz when an unused quiz is deleted (UC-58).
        b.HasOne(x => x.Quiz).WithMany(q => q.Rules).HasForeignKey(x => x.QuizId).OnDelete(DeleteBehavior.Cascade);

        b.HasCheck("question_count", "[question_count] >= 1");
    }
}
