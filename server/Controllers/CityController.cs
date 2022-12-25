namespace FlightAdvisorService.Controllers;

[ApiController]
[Route("/api/cities")]
public class CityController : ControllerBase
{
    private ICityService _cityService;

    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<ActionResult<CityResponseService<List<GetCityDto>>>> GetCities()
    {
        var response = await _cityService.GetCities();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CityResponseService<GetCityDto>>> CreateCity(CreateCityDto body)
    {
        var response = await _cityService.CreateCity(body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("{id}/airports")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CityResponseService<GetAirportDto>>> CreateAirportInCity(
        int id,
        CreateAirportDto body
    )
    {
        var response = await _cityService.CreateAirportInCity(id, body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("{id}/comments")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CityResponseService<GetCommetDto>>> CreateCommentInCity(int id, CreateCommentDto body)
    {
        // var response = await _cityService

        throw new NotImplementedException();
    }

    [HttpPut("{id}/comments/{cid}")]
    [ProducesResponseType(200)]
    public void UpdateCID() { }

    [HttpDelete("{id}/comments/{cid}")]
    [ProducesResponseType(200)]
    public void DeleteCID() { }

    [HttpPost("search")]
    [ProducesResponseType(200)]
    public void SearchCities() { }

    [HttpGet("travel")]
    [ProducesResponseType(200)]
    public void GetTravel() { }

    [HttpGet("upcoming")]
    [ProducesResponseType(200)]
    public void GetUpcoming() { }
}
