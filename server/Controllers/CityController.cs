using Microsoft.AspNetCore.Authorization;

namespace FlightAdvisorService.Controllers;

[Authorize]
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

    [HttpPost("{cid}/comments")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CityResponseService<GetCommetDto>>> CreateCommentInCity(
        int cid,
        CreateCommentDto body
    )
    {
        var response = await _cityService.CreateCommentInCity(cid, body);

        return Ok(response);
    }

    [HttpPut("{cid}/comments/{cmid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CityResponseService<GetCommetDto>>> UpdateCommentInCity(
        int cid,
        int cmid,
        UpdateCommnetDto body
    )
    {
        var response = await _cityService.UpdateCommentInCity(cid, cmid, body);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{cid}/comments/{cmid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CityResponseService<GetCommetDto>>> DeleteComment(
        int cid,
        int cmid
    )
    {
        var response = await _cityService.DeleteCommentInCity(cid, cmid);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost("search")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CityResponseService<GetCityDto>>> SearchCity(SearchCityDto body)
    {
        var response = await _cityService.SearchCity(body);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpGet("travel")]
    [ProducesResponseType(200)]
    public void GetTravel()
    {
        // var response = await _cityService.SearchCity();
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(200)]
    public async Task GetUpcoming()
    {
        var response = await _cityService.GetUpcomingTrips();
    }
}