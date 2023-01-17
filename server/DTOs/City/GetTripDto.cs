namespace FlightAdvisorService.DTOs;

public class GetTripDto
{
    public int Id { get; set; }
    public GetAirportDto? Start { get; set; }
    public List<GetAirportDto> Through { get; set; } = new List<GetAirportDto>();
    public GetAirportDto? End { get; set; }
    // public Price? Price { get; set; }
    // public Distance? Distance { get; set; }
}

public class Price
{
    public Double Total { get; set; }
    public string Currency { get; } = "AUD";
}

public class Distance
{
    public Double Total { get; set; }
    public string In { get; } = "KM";
}
