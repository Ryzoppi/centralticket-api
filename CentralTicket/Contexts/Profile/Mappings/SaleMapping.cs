using CentralTicket.Contexts.Profile.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Profile.Mappings
{
    public class SaleMapping : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("profile_Sales");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.PaymentMethod).HasColumnName("PaymentMethod");
            builder.Property(s => s.Status).HasColumnName("Status");

            builder.OwnsOne(s => s.TotalValue, p =>
                p.Property(x => x.Value).HasColumnName("TotalValue").HasColumnType("decimal(18,2)"));

            builder.OwnsOne(s => s.OrderCode, p =>
                p.Property(x => x.Value).HasColumnName("OrderCode"));

            builder.HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey("CustomerId");

            builder.HasMany(s => s.PurchasedTickets)
                .WithOne()
                .HasForeignKey("SaleId");
        }
    }
}