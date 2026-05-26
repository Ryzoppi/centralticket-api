using CentralTicket.Contexts.Auth.Entities;
using CentralTicket.Contexts.Auth.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CentralTicket.Contexts.Auth.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            // TELL EF CORE HOW TO HANDLE THE EMAIL OBJECT:
            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasConversion(
                    email => email.Value,          // How it goes TO the database (converts Email object to string)
                    value => new Email(value)      // How it comes FROM the database (creates Email object from string)
                )          
                .IsRequired();

            
            modelBuilder.Entity<User>()
                .Property(user => user.Name)
                .HasConversion(
                    name => name.Value,          // Como vai PARA o banco (converte o objeto Name para string)
                    value => new Name(value)     // Como VOLTA do banco (instancia o objeto Name a partir da string)
                )
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
