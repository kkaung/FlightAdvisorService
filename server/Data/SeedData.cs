using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Data;

public class SeedData
{
    public SeedData() { }

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (
            var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()
            )
        )
        {
            if (context.Cities.Any() || context.Ariports.Any())
                return;

            var env = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            var path = Path.Combine(env.WebRootPath, "Data", "CitySeed.json");

            var jsonString = System.IO.File.ReadAllText(path);

            if (jsonString is null)
                return;

            City city = System.Text.Json.JsonSerializer.Deserialize<City>(jsonString)!;

            if (city is not null)
                context.Cities.Add(city);
            context.SaveChanges();
        }
    }
}
