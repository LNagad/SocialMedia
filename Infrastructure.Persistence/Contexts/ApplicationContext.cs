using Core.Domain.Common;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FLUENT API

            #region tables
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region "primary keys"
            modelBuilder.Entity<User>().HasKey(p => p.Id);
            #endregion

            #region "relation Ships"

            #endregion

            #region "tables properties"

            #region Users
            modelBuilder.Entity<User>().Property(p => p.UserName).IsRequired();
                modelBuilder.Entity<User>().HasIndex(p => p.UserName).IsUnique();
                modelBuilder.Entity<User>().Property(p => p.Name).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.LastName).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.Phone).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.ProfileImg).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.Email).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.Password).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.Enabled).IsRequired();
                modelBuilder.Entity<User>().Property(p => p.Enabled).HasDefaultValue(false);

            #endregion

            #endregion

        }
    }
}
