using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("Users");
        b.HasKey(x => x.Id);
        b.Property(x => x.Email).HasMaxLength(FieldLength.Email);
        b.HasIndex(x => x.Email).IsUnique();
        b.Property(x => x.PasswordHash).IsAscii(FieldLength.PasswordHash);
        b.Property(x => x.FullName).HasMaxLength(FieldLength.FullName);
        b.HasCheck("full_name", $"LEN([full_name]) >= {FieldLength.FullNameMin}");
        b.HasEnumColumn(x => x.Gender, 6);
        b.Property(x => x.Mobile).IsAscii(FieldLength.Mobile);
        b.HasCheck("mobile", "[mobile] LIKE '0[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'");
        b.Property(x => x.Address).HasMaxLength(FieldLength.Address);
        b.Property(x => x.Avatar).HasMaxLength(FieldLength.FilePath);
        b.Property(x => x.RoleId).HasColumnName("role");
        b.HasEnumColumn(x => x.Status, 10, UserStatus.Unverified);
        b.Property(x => x.CreatedAt).HasUtcNowDefault();
    }
}
