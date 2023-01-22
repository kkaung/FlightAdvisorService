using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Airport> Airports { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Trip> Trips { get; set; }

    public string DbPath { get; }

    public DataContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            @"Host=containers-us-west-173.railway.app;Port=6734;Username=postgres;Password=ZlsFh3Po7SQD2b6Jgcyb;Database=railway"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
