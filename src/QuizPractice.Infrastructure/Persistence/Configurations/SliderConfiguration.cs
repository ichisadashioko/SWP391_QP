using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> b)
    {
        b.ToTable("Sliders");
        b.HasKey(x => x.Id);
        b.Property(x => x.Title).HasMaxLength(FieldLength.Name);
        b.Property(x => x.Image).HasMaxLength(FieldLength.FilePath);
        b.Property(x => x.Backlink).HasMaxLength(FieldLength.Url);
        b.HasCheck("backlink", "[backlink] LIKE 'https://%'");
        b.HasCheck("display_order", "[display_order] >= 1");
        b.Property(x => x.Notes).HasMaxLength(FieldLength.Brief);
        b.HasEnumColumn(x => x.Status, 10, VisibilityStatus.Hidden);
    }
}
