using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisorService.Controllers;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<UserResponseService<List<GetUserDto>>>> GetUsers()
    {
        var response = await _userService.GetUsers();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseService<GetUserDto>>> GetUser(int id)
    {
        var response = await _userService.GetUser(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponseService<GetUserDto>>> UpdateUser(
        int id,
        UpdateUserDto body
    )
    {
        var response = await _userService.UpdateUser(id, body);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserResponseService<int>>> DeleteUser(int id)
    {
        var response = await _userService.DeleteUser(id);

        if (!response.Success)
            return NotFound(response);

        return Ok(response);
    }
}
