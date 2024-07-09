using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Parking.Server.Abstractions.Models;

namespace Parking.Domain;

public class AplicationDbContext(IConfiguration configuration, DbContextOptions options) : DbContext(options)
{
    private IConfiguration _configuration = configuration;
    public DbSet<Parkings>? Parkings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseMySQL(connectionString!);
    }
}
