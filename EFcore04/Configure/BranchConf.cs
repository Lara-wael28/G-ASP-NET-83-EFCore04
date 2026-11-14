using EFcore04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EFcore04.Configure
{
    public class BranchConf : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.code);

            builder.Property(b => b.code)
                .HasMaxLength(20);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Address)
                .HasMaxLength(200);

            builder.Property(b => b.PhoneNumber)
                .HasMaxLength(30);

            builder.HasOne(b => b.Manager)
                  .WithOne(m => m.branch)
                  .HasForeignKey<Branch>(b => b.ManagerId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new Branch
               {
                   code = "BR001",
                   Name = "Cairo Main Branch",
                   Address = "Downtown Cairo",
                   PhoneNumber = "01000000001",
                   ManagerId = 1
               },

               new Branch
               {
                   code = "BR002",
                   Name = "Giza Branch",
                   Address = "Giza Square",
                   PhoneNumber = "01000000002",
                   ManagerId = 2
               }
           );

        }
    }
}
