using CRM.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;

namespace CRM.DAL.Realization.Contexts
{
    public abstract class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Base> Bases { get; set; }
        public DbSet<BaseItem> BaseItems { get; set; }
        public DbSet<BaseUser> BaseUsers { get; set; }

        protected IConfiguration Configuration;

        public Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected abstract override void OnConfiguring(DbContextOptionsBuilder optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Base>()
                .HasMany(e => e.BaseItems)
                .WithOne(e => e.Base)
                .HasForeignKey(e => e.BaseId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Bases)
                .WithMany(b => b.Users)
                .UsingEntity<BaseUser>(
                    ub => ub.HasOne(ub => ub.Base).WithMany().HasForeignKey(ub => ub.BasesId),
                    ub => ub.HasOne(ub => ub.User).WithMany().HasForeignKey(ub => ub.UsersId),
                    ub =>
                    {
                        ub.HasKey(ub => new { ub.UsersId, ub.BasesId });
                        ub.ToTable("BaseUser");
                    }
                );
        }
    }
}
