namespace FlightAdvisorService.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, ResponseRegisterDto>();
        CreateMap<User, GetUserDto>();
        CreateMap<City, GetCityDto>();
        CreateMap<CreateCityDto, City>();
        CreateMap<CreateAirportDto, Ariport>();
        CreateMap<Ariport, GetAirportDto>();
        CreateMap<Comment, GetCommetDto>().ReverseMap();
        CreateMap<CreateCommentDto, Comment>().ReverseMap();
        CreateMap<Trip, GetTripDto>().ReverseMap();
    }
}
