namespace FlightAdvisorService.DTOs;

public class CreateTripDto
{
    public string Start { get; set; } = String.Empty;
    public string End { get; set; } = String.Empty;
    public List<string> Through { get; set; } = new List<string>();
    public double price { get; set; }
}

