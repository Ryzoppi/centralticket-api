using CentralTicket.Contexts.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CentralTicket.Contexts.Auth.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("auth_Users");
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(x => x.Value).HasColumnName("Name");
            });

            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(x => x.Value).HasColumnName("Email");
            });

            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(x => x.Value).HasColumnName("PasswordHash");
            });
        }
    }
}
