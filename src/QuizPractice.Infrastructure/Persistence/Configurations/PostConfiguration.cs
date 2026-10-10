using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> b)
    {
        b.ToTable("Posts");
        b.HasKey(x => x.Id);
        b.Property(x => x.CategoryId).HasColumnName("category");
        b.Property(x => x.Title).HasMaxLength(FieldLength.Name);
        b.Property(x => x.Thumbnail).HasMaxLength(FieldLength.FilePath);
        b.Property(x => x.Brief).HasMaxLength(FieldLength.Brief);
        b.Property(x => x.IsFeatured).HasDbDefault(false);
        b.HasEnumColumn(x => x.Status, 10, VisibilityStatus.Hidden);
        b.Property(x => x.UpdatedAt).HasUtcNowDefault();
    }
}
