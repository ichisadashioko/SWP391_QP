using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class AttemptConfiguration : IEntityTypeConfiguration<Attempt>
{
    public void Configure(EntityTypeBuilder<Attempt> b)
    {
        b.ToTable("Attempts");
        b.HasKey(x => x.Id);
        b.HasEnumColumn(x => x.Mode, 10);
        b.HasEnumColumn(x => x.SelectionType, 10);
        b.HasCheck("question_count", "[question_count] BETWEEN 1 AND 100");
        b.Property(x => x.CurrentOrder).HasDbDefault((short)1);
        b.Property(x => x.StartedAt).HasUtcNowDefault();
        b.Property(x => x.Score).HasPrecision(5, 2);
        b.HasCheck("score", "[score] BETWEEN 0 AND 100");
        b.HasCheck("pass_rate", "[pass_rate] BETWEEN 0 AND 100");
        b.HasEnumColumn(x => x.Status, 12, AttemptStatus.InProgress);
    }
}
