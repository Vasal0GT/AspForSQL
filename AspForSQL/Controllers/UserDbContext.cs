using AspForSQL.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace AspForSQL.Controllers
{
    public class UserDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public DbSet<User> Users { get; set; }

        public UserDbContext(IConfiguration config)
        { 
            Configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ApiDatabase"));
        }
    }
}
