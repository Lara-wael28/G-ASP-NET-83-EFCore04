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
    public class ManagerConf : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(m => m.Email)
                   .HasMaxLength(200);

            builder.Property(m => m.PhoneNumber)
                   .HasMaxLength(30);
            builder.HasData(
               new Manager
               {
                   Id = 1,
                   FullName = "Ahmed Hassan",
                   Email = "ahmed@bank.com",
                   PhoneNumber = "01111111111",
                   HireDate = new DateTime(2020, 1, 1)
               },

               new Manager
               {
                   Id = 2,
                   FullName = "Mona Ali",
                   Email = "mona@bank.com",
                   PhoneNumber = "01222222222",
                   HireDate = new DateTime(2021, 2, 1)
               }
           );
        }
    }
}
