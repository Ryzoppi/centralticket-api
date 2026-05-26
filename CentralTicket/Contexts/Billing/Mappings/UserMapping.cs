using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralTicket.Contexts.Billing.Entities;

namespace CentralTicket.Contexts.Billing.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("billing_Users");
            builder.HasKey(u => u.Id);
        }
    }
}