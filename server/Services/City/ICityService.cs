namespace FlightAdvisorService.Services;

public interface ICityService
{
    Task<CityResponseService<List<GetCityDto>>> GetCities();
    Task<CityResponseService<GetCityDto>> CreateCity(CreateCityDto body);
    Task<CityResponseService<GetAirportDto>> CreateAirportInCity(int id, CreateAirportDto body);
    Task<CityResponseService<GetCommetDto>> CreateCommentInCity(int id, CreateCommentDto body);

    Task<CityResponseService<GetCommetDto>> UpdateCommentInCity(
        int id,
        int cid,
        UpdateCommnetDto body
    );

    Task<CityResponseService<GetCommetDto>> DeleteCommentInCity(int id, int cid);

    Task<CityResponseService<GetCityDto>> SearchCity();

    Task<CityResponseService<GetCityDto>> GetTravel();

    Task<CityResponseService<GetCityDto>> GetUpcomingTrips();
}
