using Calabonga.Facts.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Calabonga.Facts.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly); //сработала

            //builder.ApplyConfiguration(new FactModelConfiguration());
            //builder.ApplyConfiguration(new TagModelConfiguration());
        }
    }
}
