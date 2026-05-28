using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CentralTicket.Contexts.Profile.Entities;

namespace CentralTicket.Contexts.Profile.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("profile_Users");
            builder.HasKey(u => u.Id);

            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(x => x.Value).HasColumnName("Name");
            });

            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(x => x.Value).HasColumnName("Email");
            });

            builder.OwnsOne(u => u.Password, e =>
            {
                e.Property(x => x.Value).HasColumnName("Password");
            });

            builder.Property(e => e.ProfilePictureUrl).HasColumnName("ProfilePictureUrl");
        }
    }
}