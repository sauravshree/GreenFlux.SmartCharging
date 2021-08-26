using GreenFlux.SmartCharging.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.SmartCharging.Infrastructure.EntityFramework
{
    public class SmartChargingDbContext : DbContext
    {
        public SmartChargingDbContext(DbContextOptions<SmartChargingDbContext> options) : base(options)
        {

        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ChargeStation> ChargeStations { get; set; }
        public DbSet<Connector> Connectors { get; set; }
    }
}
