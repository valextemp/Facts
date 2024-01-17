using Calabonga.EntityFrameworkCore.Entities.Base;
using Calabonga.Facts.Web.Data.Base;
using Calabonga.Facts.Web.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Calabonga.Facts.Web.Data
{
    public class ApplicationDbContext : DbContextBase
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fact> Facts { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }


}
