namespace FlightAdvisorService.Models;

public class Trip
{
    public int Id { get; set; }
    public int StartAriportId { get; set; }
    public int EndAirportId { get; set; }
    public List<Airport> ThroughAirport { get; set; } = new List<Airport>();
    public double TotalPrice { get; set; }
    public double TotalDistance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
