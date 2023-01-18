namespace FlightAdvisorService.DTOs;

public class GetTripDto
{
    public int Id { get; set; }
    public GetAirportDto? Start { get; set; }
    public List<GetAirportDto> Through { get; set; } = new List<GetAirportDto>();
    public GetAirportDto? End { get; set; }
    public PriceDto Price { get; set; } = new PriceDto();
    public DistanceDto Distance { get; set; } = new DistanceDto();
}

public class PriceDto
{
    public Double Total { get; set; }
    public string Currency { get; } = "AUD";
}

public class DistanceDto
{
    public Double Total { get; set; }
    public string In { get; } = "KM";
}
