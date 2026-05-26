using Microsoft.EntityFrameworkCore;
using CentralTicket.Contexts.Profile.Entities;
using CentralTicket.Contexts.Profile.Mappings;

namespace CentralTicket.Contexts.Profile.Data
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options)
        {
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("profile");
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new SaleMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}