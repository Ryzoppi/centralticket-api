using CentralTicket.Contexts.Billing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Billing.Mappings
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("billing_Tickets");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Category).HasColumnName("Category");
            builder.Property(t => t.Kind).HasColumnName("Kind");
            builder.Property(t => t.Status).HasColumnName("Status");

            builder.OwnsOne(t => t.Value, p =>
                p.Property(x => x.Value).HasColumnName("Value").HasColumnType("decimal(18,2)"));

            builder.HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey("EventId");
        }
    }
}