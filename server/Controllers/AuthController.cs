using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisorService.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseService<ResponseRegisterDto>>> Register(
        RegisterDto body
    )
    {
        var response = await _authService.Register(body);

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseService<string>>> Login(LoginDtos body) {
        var response = await _authService.Login(body);

        if(!response.Success) 
            return Unauthorized(response);

        return Ok(response);
    }
}
