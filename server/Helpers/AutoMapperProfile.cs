namespace FlightAdvisorService.Helpers;

public class AutoMapperProfile : Profile { 
    public AutoMapperProfile() {
        CreateMap<RegisterDto, User>();
        CreateMap<User, ResponseRegisterDto>();
        CreateMap<User, GetUserDto>();
    }
}
