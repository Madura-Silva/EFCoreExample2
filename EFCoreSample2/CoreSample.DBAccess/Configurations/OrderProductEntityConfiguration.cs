using CoreSample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSample.DBAccess.Configurations
{
    public class OrderProductEntityConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.ToTable("OrderProduct");
            builder.HasKey(x => new { x.OrderId, x.ProductId});
            builder.HasOne(x => x.Order).WithMany(x => x.OrderProducts);
            builder.HasOne(x => x.Product).WithMany(x => x.OrderProducts);

        }
    }
}
