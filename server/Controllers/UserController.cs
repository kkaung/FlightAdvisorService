using Microsoft.AspNetCore.Mvc;

namespace FlightAdvisorService.Controllers;

[ApiController]
[Route("api/users")]
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

        return response;
    }
}
