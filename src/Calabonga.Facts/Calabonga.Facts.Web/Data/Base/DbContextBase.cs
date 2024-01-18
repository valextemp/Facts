using Calabonga.EntityFrameworkCore.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Calabonga.UnitOfWork;

namespace Calabonga.Facts.Web.Data.Base
{
    public abstract class DbContextBase : IdentityDbContext
    {
        public SaveChangesResult SaveChangesResult { get; set; }
        protected DbContextBase(DbContextOptions options)
            : base(options)
        {
            SaveChangesResult = new SaveChangesResult();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly); //сработала

            //builder.ApplyConfiguration(new FactModelConfiguration());
            //builder.ApplyConfiguration(new TagModelConfiguration());
        }
        public override int SaveChanges()
        {
            DbSaveChanges();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DbSaveChanges();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            DbSaveChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            DbSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void DbSaveChanges()
        {
            const string defaultUser = "System";
            var defaultDate = DateTime.UtcNow;
            //Added Entities
            var addedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entry in addedEntities)
            {
                if (entry.Entity is IAuditable)
                {
                    continue;
                }
                var createdAt = entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue;
                var updatedAt = entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue;
                var createdBy = entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue;
                var updatedBy = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;


                if (string.IsNullOrEmpty(createdBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.CreatedBy)).CurrentValue = defaultUser;
                }
                if (string.IsNullOrEmpty(updatedBy?.ToString()))
                {
                    entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = defaultUser;
                }
                if (DateTime.Parse(createdAt?.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.CreatedAt)).CurrentValue = defaultDate;
                }
                if (updatedAt != null && DateTime.Parse(updatedAt.ToString()!).Year < 1970)
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }
                else
                {
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue = defaultDate;
                }

                SaveChangesResult.AddMessage("Some entities were created");


            }

            //Modified Entities
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entry in modifiedEntities)
            {
                if (entry.Entity is IAuditable)
                {
                    var userName = entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue == null
                        ? defaultUser
                        : entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue;
                    entry.Property(nameof(IAuditable.UpdatedAt)).CurrentValue= defaultDate;
                    entry.Property(nameof(IAuditable.UpdatedBy)).CurrentValue = userName;

                }
                
            }
        }
    }
}
