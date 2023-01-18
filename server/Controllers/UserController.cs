using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace FlightAdvisorService.Controllers;

[EnableCors("AllowSpecificOrigin")]
[Authorize]
[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("me")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetMe()
    {
        var response = await _userService.GetMe();

        if (!response.Success)
            return BadRequest(response);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetUsers()
    {
        var response = await _userService.GetUsers();

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(int id)
    {
        var response = await _userService.GetUser(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateUser(
        int id,
        UpdateUserDto body
    )
    {
        var response = await _userService.UpdateUser(id, body);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<int>>> DeleteUser(int id)
    {
        var response = await _userService.DeleteUser(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}
