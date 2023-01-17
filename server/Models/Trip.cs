using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.DTOs;

[Keyless]
public class Trip
{
    public int Id { get; set; }
    public int StartAirportId { get; set; }
    public List<int> ThroughAirportIds { get; set; } = new List<int>();
    public int EndAirportId { get; set; }
    public double Price { get; set; }
    public double Distance { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
