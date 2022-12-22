using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Data;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public string DbPath { get; }

    public DataContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "flightAdvisor.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");
}
