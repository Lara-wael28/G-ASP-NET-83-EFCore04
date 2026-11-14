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
    public class CustomerConf : IEntityTypeConfiguration<Customer>
    {
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.NationalId)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Email)
                   .HasMaxLength(200);

            builder.Property(c => c.PhoneNumber)
                   .HasMaxLength(30);

            builder.Property(c => c.Address)
                   .HasMaxLength(250);

            builder.Property(c => c.CustomerType)
                   .HasMaxLength(30);
        }
    }
    }
