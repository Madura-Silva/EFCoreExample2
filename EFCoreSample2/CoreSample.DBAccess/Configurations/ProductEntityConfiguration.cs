using System;
using System.Collections.Generic;
using System.Text;
using CoreSample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreSample.DBAccess.Configurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CurrentPrice).HasColumnType("decimal(5,2)");
            builder.HasMany(x => x.OrderProducts).WithOne(x => x.Product);
        }
    }
}
