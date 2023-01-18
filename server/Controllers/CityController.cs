using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace FlightAdvisorService.Controllers;

[EnableCors("AllowSpecificOrigin")]
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
    public async Task<ActionResult<ServiceResponse<List<GetCityDto>>>> GetCities()
    {
        var response = await _cityService.GetCities();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceResponse<GetCityDto>>> CreateCity(CreateCityDto body)
    {
        var response = await _cityService.CreateCity(body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("airports")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceResponse<GetAirportDto>>> GetAirportsInCity(
        [FromQuery] string byName
    )
    {
        var response = await _cityService.GetAirportsInCity(byName);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("{id}/airports")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceResponse<GetAirportDto>>> CreateAirportInCity(
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
    public async Task<ActionResult<ServiceResponse<GetCommetDto>>> CreateCommentInCity(
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
    public async Task<ActionResult<ServiceResponse<GetCommetDto>>> UpdateCommentInCity(
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
    public async Task<ActionResult<ServiceResponse<GetCommetDto>>> DeleteComment(int cid, int cmid)
    {
        var response = await _cityService.DeleteCommentInCity(cid, cmid);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("search")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ServiceResponse<List<GetCityDto>>>> SearchCity(
        [FromQuery(Name = "cLimit")] string? cLimit,
        SearchCityDto body
    )
    {
        var response = await _cityService.SearchCity(body, Convert.ToInt32(cLimit));

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost("travel")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceResponse<GetTripDto>>> CreateTrip(CreateTripDto body)
    {
        var response = await _cityService.CreateTrip(body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("travel")]
    [ProducesResponseType(200)]
    public async Task<ActionResult<ServiceResponse<List<GetTripDto>>>> GetTravel(
        [FromQuery(Name = "from")] string from,
        [FromQuery(Name = "to")] string to
    )
    {
        var response = await _cityService.GetTrips(from, to);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpGet("upcoming")]
    [ProducesResponseType(200)]
    public async Task GetUpcoming()
    {
        var response = await _cityService.GetUpcomingTrips();
    }
}
