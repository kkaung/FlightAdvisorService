namespace FlightAdvisorService.Services;

public interface IUserService
{
    Task<ServiceResponse<List<GetUserDto>>> GetUsers();
    Task<ServiceResponse<GetUserDto>> GetUser(int id);
    Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto body);
    Task<ServiceResponse<int>> DeleteUser(int id);
}
