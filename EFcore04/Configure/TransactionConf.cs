using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFcore04.Models;

namespace EFcore04.Configurations
{
    public class TransactionConf : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionNumber);

            builder.Property(t => t.Amount)
                   .HasPrecision(18, 2);

            builder.Property(t => t.TransactionType)
                   .HasMaxLength(50);

            builder.Property(t => t.Note)
                   .HasMaxLength(500);

            builder.HasOne(t => t.Account)
                   .WithMany(a => a.Transactions)
                   .HasForeignKey(t => t.AccountNumber)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}