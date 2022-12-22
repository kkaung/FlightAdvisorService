namespace FlightAdvisorService.Services;

public interface IAuthService
{
    Task<AuthResponseService<ResponseRegisterDto>> Register(RegisterDto body);

    Task<AuthResponseService<string>> Login(LoginDtos body);
}
