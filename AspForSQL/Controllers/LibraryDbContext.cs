using Microsoft.EntityFrameworkCore;

namespace AspForSQL.Controllers
{
    public class LibraryDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public LibraryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ApiDatabase"));
        }
        public DbSet<Library> Libraries { get; set; }
    }
}
