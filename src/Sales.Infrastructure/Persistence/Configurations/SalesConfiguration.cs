using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Infrastructure.Persistence.Configurations
{
    public class SalesConfiguration : IEntityTypeConfiguration<Domain.Entities.Sales>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Sales> builder)
        {
            builder.Property(s => s.Region)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(s => s.Country)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.ItemType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SalesChannel)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.OrderPriority)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(s => s.OrderDate)
                .IsRequired();

            builder.Property(s => s.OrderID)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(s => s.ShipDate)
                .IsRequired();

            builder.Property(s => s.UnitsSold)
                .IsRequired();

            builder.Property(s => s.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.UnitCost)
                .IsRequired()
                 .HasPrecision(18, 2);

            builder.Property(s => s.TotalCost)
               .IsRequired()
               .HasPrecision(18, 2);

            builder.Property(s => s.TotalRevenue)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.TotalProfit)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.CreatedBy)
               .HasMaxLength(50);

            builder.Property(s => s.LastModifiedBy)
               .HasMaxLength(50);

        }
    }
}
