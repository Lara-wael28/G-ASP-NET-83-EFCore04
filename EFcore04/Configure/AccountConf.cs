using EFcore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFcore04.Configure
{
    public class AccountConf : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
                builder.HasKey(a => a.AccountNumber);

                builder.Property(a => a.AccountNumber)
                       .HasMaxLength(30);

                builder.Property(a => a.AccountType)
                       .IsRequired()
                       .HasMaxLength(50);

                builder.Property(a => a.CurrentBalance)
                       .HasPrecision(18, 2);

                builder.HasOne(a => a.Branch)
                       .WithMany(b => b.Accounts)
                       .HasForeignKey(a => a.BranchCode)
                       .OnDelete(DeleteBehavior.Restrict);
            }
        }
    }

