using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> b)
    {
        b.ToTable("Lessons");
        b.HasKey(x => x.Id);
        b.Property(x => x.TypeId).HasColumnName("type");
        b.HasOne(x => x.Topic).WithMany().HasForeignKey(x => x.TopicId);
        b.Property(x => x.Name).HasMaxLength(FieldLength.Name);
        b.HasCheck("display_order", "[display_order] >= 1");
        b.Property(x => x.VideoUrl).HasMaxLength(FieldLength.Url);
        b.HasCheck("video_url", "[video_url] LIKE 'https://%'");
        b.HasEnumColumn(x => x.Status, 10, ActiveStatus.Active);
    }
}
