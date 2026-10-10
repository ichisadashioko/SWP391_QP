using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> b)
    {
        b.ToTable("Questions");
        b.HasKey(x => x.Id);
        b.Property(x => x.LevelId).HasColumnName("level");
        b.Property(x => x.Media).HasMaxLength(FieldLength.Url);
        b.HasEnumColumn(x => x.Status, 10, VisibilityStatus.Shown);
    }
}
