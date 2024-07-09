using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Parking.Server.Abstractions.Models;

namespace Parking.Infra
{
    public class AplicationDbContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Parkings>? Parking { get; set; }

        public AplicationDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySQL(connectionString!);
        }
    }
}
