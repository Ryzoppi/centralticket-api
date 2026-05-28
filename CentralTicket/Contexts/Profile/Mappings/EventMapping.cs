using CentralTicket.Contexts.Profile.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Profile.Mappings
{
    public class EventMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("profile_Events");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description).HasColumnName("Description");
            builder.Property(e => e.StartDate).HasColumnName("StartDate");
            builder.Property(e => e.EndDate).HasColumnName("EndDate");
            builder.Property(e => e.Status).HasColumnName("Status");
            builder.Property(e => e.Title).HasColumnName("Title");
            builder.Property(e => e.TicketAmount).HasColumnName("TicketAmount");

        }
    }
}