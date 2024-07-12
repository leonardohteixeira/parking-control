using Microsoft.EntityFrameworkCore;
using Parking.Domain.Abstractions.Models;
using Parking.Server.Abstractions.Models;

namespace Parking.Domain
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Parkings>? Parkings { get; set; }
        public DbSet<ParkingValues>? ParkingValues { get; set; }
    }
}

