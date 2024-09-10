using Microsoft.EntityFrameworkCore;
using Model;

namespace DataBaseService
{
    public class SprinklerAppDbContext : DbContext
    {
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<WeatherRoot> WeatherRoots { get; set; }
        public DbSet<Minutely> Minutelies { get; set; }
        public DbSet<Hourly> Hourlies { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<LocationInfo> LocationInfos { get; set; }
        public DbSet<LocalNames> LocalNames { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public SprinklerAppDbContext()
        {
            
        }

        public SprinklerAppDbContext(DbContextOptions<SprinklerAppDbContext> options): base(options) 
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //Todo: add secret 
        //    optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=SprinklerAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //}
    }
}
