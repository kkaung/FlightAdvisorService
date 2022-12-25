namespace FlightAdvisorService.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Country { get; set; } = "AU";
    public List<Comment> Comments { get; set; } = Array.Empty<Comment>().ToList();
    public List<Ariport> Airports { get; set; } = Array.Empty<Ariport>().ToList();
}

public class Comment
{
    public int Id { get; set; }
    public string Description { get; set; } = String.Empty;
    public string CityId { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public string UserId { get; set; } = String.Empty;
}

public class Ariport 
{
    public int Id { get; set; }
    public string CityName { get; set; } = String.Empty;
    public string Iata { get; set;} = String.Empty;
    public string Icao { get; set; } = String.Empty;
    public int Latitude {get; set; }
    public int Longitude {get; set; }  
    public string CountryName { get; set; } = "AU";
}