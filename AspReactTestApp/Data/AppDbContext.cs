using AspReactTestApp.Entities.Concrete; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspReactTestApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() {}

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users => Set<User>();
    }
}
