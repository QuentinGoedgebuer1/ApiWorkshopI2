using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WorkShopI2.Models.Mesures;
using WorkShopI2.Models.Parks;
using WorkShopI2.Models.Villes;
using WorkShopI2.Models.WeatherForecast;

namespace WorkShopI2
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        public DbSet<Ville> Villes { get; set; }

        public DbSet<Park> Parks { get; set; }

        public DbSet<Mesure> Mesures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PARK
            modelBuilder.Entity<Park>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Park>()
                .HasOne(x => x.Villes)
                .WithMany(x => x.Parks)
                .HasForeignKey(x => x.VillesId);

            // VILLE
            modelBuilder.Entity<Ville>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Ville>()
                .HasMany(x => x.Parks)
                .WithOne(x => x.Villes)
                .HasForeignKey(x => x.VillesId);

            // MESURE
            modelBuilder.Entity<Mesure>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Mesure>()
                .HasOne(x => x.Park)
                .WithMany(x => x.Mesures)
                .HasForeignKey(x => x.ParkId);




            // Code to seed data
        }
    }
}
