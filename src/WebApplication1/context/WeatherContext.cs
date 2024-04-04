using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace WebApplication1.context;

public class WeatherContext: DbContext
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    public string DbPath { get; }
    
    public WeatherContext(DbContextOptions<WeatherContext> options): base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<WeatherForecast>()
            .Property(b => b.Id)
            .HasValueGenerator<GuidValueGenerator>();
    }
    
}