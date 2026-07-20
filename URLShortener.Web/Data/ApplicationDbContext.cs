using Microsoft.EntityFrameworkCore;
using URLShortener.Web.Entities;

namespace URLShortener.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options ) : base( options ) 
        { 
        }

        public DbSet<UrlEntry> Urls => Set<UrlEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
