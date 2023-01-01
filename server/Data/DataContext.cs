using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightAdvisorService.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Ariport> Ariports { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public string DbPath { get; }

    public DataContext(DbContextOptions<DataContext> dbContextOptions)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var fileName = "flightAdvisor.db!";

        DbPath = Path.Join(path, fileName);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // DeleteDBFile();
    }

    private void DeleteDBFile()
    {
        if (File.Exists(DbPath))
        {
            File.Delete(DbPath);
            Console.WriteLine("Successfully deleted!");
        }
    }
}
