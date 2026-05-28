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
            builder.Property(s => s.OrderCode).HasColumnName("OrderCode");
            builder.Property(s => s.TotalValue).HasColumnName("TotalValue");

            builder.HasOne(s => s.Customer)
                .WithMany()
                .HasForeignKey("CustomerId");

            builder.HasMany(s => s.PurchasedTickets)
                .WithOne()
                .HasForeignKey("SaleId");
        }
    }
}