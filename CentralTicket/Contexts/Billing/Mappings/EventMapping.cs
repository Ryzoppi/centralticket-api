using CentralTicket.Contexts.Billing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Billing.Mappings
{
    public class EventMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("billing_Events");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Property(e => e.Status).HasColumnName("Status");

            builder.OwnsOne(e => e.Title, p =>
                p.Property(x => x.Value).HasColumnName("Title"));

            builder.OwnsOne(e => e.TicketAmount, p =>
                p.Property(x => x.Value).HasColumnName("TicketAmount"));
        }
    }
}