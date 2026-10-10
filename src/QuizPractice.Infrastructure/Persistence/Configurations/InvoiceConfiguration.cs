using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;
using QuizPractice.Domain.Enums;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> b)
    {
        b.ToTable("Invoices");
        b.HasKey(x => x.Id);
        b.Property(x => x.ConfirmedById).HasColumnName("confirmed_by");
        b.Property(x => x.Code).IsAscii(FieldLength.InvoiceCode, fixedLength: true);
        b.HasIndex(x => x.Code).IsUnique();
        b.HasCheck("total_amount", "[total_amount] > 0");
        b.Property(x => x.BankName).HasMaxLength(FieldLength.BankName);
        b.Property(x => x.BankBin).IsAscii(FieldLength.BankBin, fixedLength: true);
        b.Property(x => x.AccountNumber).IsAscii(FieldLength.AccountNumber);
        b.HasCheck("account_number", "[account_number] NOT LIKE '%[^0-9]%'");
        b.Property(x => x.AccountName).HasMaxLength(FieldLength.AccountName);

        // Concurrency token: a confirmation and a cancellation cannot both win (BR-24).
        b.HasEnumColumn(x => x.Status, 10, InvoiceStatus.Unpaid).IsConcurrencyToken();

        b.Property(x => x.CreatedAt).HasUtcNowDefault();
        b.Property(x => x.Notes).HasMaxLength(FieldLength.Brief);
    }
}
