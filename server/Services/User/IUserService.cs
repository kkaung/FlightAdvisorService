namespace FlightAdvisorService.Services;

public interface IUserService
{
    Task<UserResponseService<List<GetUserDto>>> GetUsers();
}
