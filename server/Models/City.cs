namespace FlightAdvisorService.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string Country { get; set; } = "AU";
    public List<Comment>? Comments { get; }
    public List<Ariport>? Airports { get; }
}
