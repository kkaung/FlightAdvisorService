namespace FlightAdvisorService.DTOs;

public class CreateAirportDto
{
    public string Name { get; set; } = String.Empty;
    public string Iata { get; set; } = String.Empty;
    public string Icao { get; set; } = String.Empty;
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
