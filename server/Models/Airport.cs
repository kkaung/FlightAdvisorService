namespace FlightAdvisorService.Models;

public class Airport
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Iata { get; set; } = String.Empty;
    public string Icao { get; set; } = String.Empty;
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string Country { get; set; } = "AU";
    public int CityId { get; set; }

    public List<Trip>? Trips { get; set; }
}
