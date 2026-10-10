using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class QuestionDimensionConfiguration : IEntityTypeConfiguration<QuestionDimension>
{
    public void Configure(EntityTypeBuilder<QuestionDimension> b)
    {
        b.ToTable("QuestionDimensions");
        b.HasKey(x => new { x.QuestionId, x.DimensionId });
    }
}
