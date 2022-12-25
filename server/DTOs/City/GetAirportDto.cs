namespace FlightAdvisorService.DTOs;

public class GetAirportDto
{
    public int Id { get; set; }
    public string CityName { get; set; } = String.Empty;
    public string Iata { get; set; } = String.Empty;
    public string Icao { get; set; } = String.Empty;
    public int Latitude { get; set; }
    public int Longitude { get; set; }
    public string CountryName { get; set; } = "AU";
}
