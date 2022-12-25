namespace FlightAdvisorService.Services;

public interface IUserService
{
    Task<UserResponseService<List<GetUserDto>>> GetUsers();
    Task<UserResponseService<GetUserDto>> GetUser(int id);
    Task<UserResponseService<GetUserDto>> UpdateUser(int id, UpdateUserDto body);
    Task<UserResponseService<int>> DeleteUser(int id);
}
