namespace FlightAdvisorService.Models;

public class Trip
{
    public int Id { get; set; }
    public int StartAriportId { get; set; }
    public int EndAirportId { get; set; }
    public List<Ariport> ThroughAirport { get; set; } = new List<Ariport>();
    public double TotalPrice { get; set; }
    public double TotalDistance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
