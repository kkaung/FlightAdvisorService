using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Services;

public class CityService : ICityService
{
    private IMapper _mapper;
    private DataContext _context;

    public CityService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CityResponseService<List<GetCityDto>>> GetCities()
    {
        var response = new CityResponseService<List<GetCityDto>>();

        response.Data = await _context.Cities.Select(c => _mapper.Map<GetCityDto>(c)).ToListAsync();

        return response;
    }

    public async Task<CityResponseService<GetCityDto>> CreateCity(CreateCityDto body)
    {
        var response = new CityResponseService<GetCityDto>();

        if (await CityExists(body.Name))
        {
            response.Success = false;
            response.Message = "City already registered!";
        }

        var newCity = _mapper.Map<City>(body);

        await _context.Cities.AddAsync(newCity);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCityDto>(newCity);

        return response;
    }

    public async Task<CityResponseService<GetAirportDto>> CreateAirportInCity(
        int id,
        CreateAirportDto body
    )
    {
        var response = new CityResponseService<GetAirportDto>();

        var newAirport = _mapper.Map<Ariport>(body);

        await _context.Ariports.AddAsync(newAirport);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetAirportDto>(newAirport);

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> CreateCommentInCity(
        int id,
        CreateCommentDto body
    )
    {
        var response = new CityResponseService<GetCommetDto>();

        var newComment = _mapper.Map<Comment>(body);

        await _context.Comments.AddAsync(newComment);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCommetDto>(newComment);

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> UpdateCommentInCity(
        int id,
        int cid,
        UpdateCommnetDto body
    )
    {
        var response = new CityResponseService<GetCommetDto>();

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> DeleteCommentInCity(int id, int cid)
    {
        var response = new CityResponseService<GetCommetDto>();

        throw new NotImplementedException();
    }

    public Task<CityResponseService<GetCityDto>> SearchCity()
    {
        throw new NotImplementedException();
    }

    private async Task<bool> CityExists(string name)
    {
        return await _context.Cities.AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }

    private async Task<bool> AirportExists(string name)
    {
        return await _context.Ariports.AnyAsync(a => a.CityName.ToLower() == name.ToLower());
    }

    public Task<CityResponseService<GetCityDto>> GetTravel()
    {
        throw new NotImplementedException();
    }

    public Task<CityResponseService<GetCityDto>> GetUpcomingTrips()
    {
        throw new NotImplementedException();
    }
}
