using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> b)
    {
        b.ToTable("Settings");
        b.HasKey(x => x.Id);
        b.HasEnumColumn(x => x.Type, 30);
        b.Property(x => x.Value).HasMaxLength(FieldLength.Name);
        b.HasCheck("display_order", "[display_order] >= 1");
        b.HasEnumColumn(x => x.Status, 10, ActiveStatus.Active);
        b.HasIndex(x => new { x.Type, x.Value }).IsUnique();
    }
}
