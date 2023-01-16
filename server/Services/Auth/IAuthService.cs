namespace FlightAdvisorService.Services;

public interface IAuthService
{
    Task<ServiceResponse<ResponseRegisterDto>> Register(RegisterDto body);
    
    Task<ServiceResponse<string>> Login(LoginDtos body);
}
