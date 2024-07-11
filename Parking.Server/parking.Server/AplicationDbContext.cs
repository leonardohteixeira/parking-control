using Microsoft.EntityFrameworkCore;
using Parking.Server.Abstractions.Models;

namespace Parking.Domain
{
    public class ApplicationDbContext(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        private readonly IConfiguration _configuration = configuration;

        public DbSet<Parkings>? Parkings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseMySQL(connectionString);
            }
        }
    }
}

