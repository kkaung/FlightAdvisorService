using Microsoft.AspNetCore.Cors;

namespace FlightAdvisorService.Controllers;

[EnableCors("AllowSpecificOrigin")]
[ApiController]
[Route("public")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceResponse<ResponseRegisterDto>>> Register(RegisterDto body)
    {
        var response = await _authService.Register(body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDtos body)
    {
        var response = await _authService.Login(body);

        if (!response.Success)
            return Unauthorized(response);

        return Ok(response);
    }
}
