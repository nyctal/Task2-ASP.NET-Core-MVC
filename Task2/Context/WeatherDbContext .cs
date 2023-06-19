using Microsoft.EntityFrameworkCore;


using Task2.Entities;

namespace Task2.Context
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<WeatherData> WeatherData { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            string connectionString = configuration.GetConnectionString("WeatherDbConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
