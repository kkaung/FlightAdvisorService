namespace FlightAdvisorService.DTOs;

public class GetCityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Country { get; set; } = "AU";
}
