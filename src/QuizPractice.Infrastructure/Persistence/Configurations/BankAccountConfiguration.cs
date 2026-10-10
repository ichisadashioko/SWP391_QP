using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizPractice.Domain.Constants;
using QuizPractice.Domain.Entities;

namespace QuizPractice.Infrastructure.Persistence.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> b)
    {
        b.ToTable("BankAccounts");
        b.HasKey(x => x.Id);

        // One row only (BR-24): no IDENTITY, and the key can only be 1.
        b.Property(x => x.Id).ValueGeneratedNever();
        b.HasCheck("bank_account_id", $"[bank_account_id] = {BankAccount.SingleId}");

        b.Property(x => x.BankName).HasMaxLength(FieldLength.BankName);
        b.Property(x => x.BankBin).IsAscii(FieldLength.BankBin, fixedLength: true);
        b.Property(x => x.AccountNumber).IsAscii(FieldLength.AccountNumber);
        b.HasCheck("account_number", "[account_number] NOT LIKE '%[^0-9]%'");
        b.Property(x => x.AccountName).HasMaxLength(FieldLength.AccountName);
        b.Property(x => x.UpdatedAt).HasUtcNowDefault().IsConcurrencyToken();
    }
}
