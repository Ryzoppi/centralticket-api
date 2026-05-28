using CentralTicket.Contexts.Profile.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Profile.Mappings
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("profile_Tickets");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Category).HasColumnName("Category");
            builder.Property(t => t.Kind).HasColumnName("Kind");
            builder.Property(t => t.Status).HasColumnName("Status");
            builder.Property(t => t.Status).HasColumnName("Status");

            builder.HasOne(t => t.Event)
                .WithMany()
                .HasForeignKey("EventId");
        }
    }
}