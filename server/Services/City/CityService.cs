using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Services;

public class CityService : ICityService
{
    private IMapper _mapper;
    private DataContext _context;
    private IHttpContextAccessor _httpContextAccess;

    public CityService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccess)
    {
        _mapper = mapper;
        _context = context;
        _httpContextAccess = httpContextAccess;
    }

    public async Task<CityResponseService<List<GetCityDto>>> GetCities()
    {
        var response = new CityResponseService<List<GetCityDto>>();

        var cities = await _context.Cities
            .Include(c => c.Airports)
            .Include(c => c.Comments)
            .ToListAsync();

        response.Data = cities.Select(c => _mapper.Map<GetCityDto>(c)).ToList();

        return response;
    }

    public async Task<CityResponseService<GetCityDto>> CreateCity(CreateCityDto body)
    {
        var response = new CityResponseService<GetCityDto>();

        if (await CityExists(body.Name))
        {
            response.Success = false;
            response.Message = "City already registered!";
            return response;
        }

        var newCity = _mapper.Map<City>(body);

        await _context.Cities.AddAsync(newCity);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCityDto>(newCity);

        return response;
    }

    public async Task<CityResponseService<GetAirportDto>> CreateAirportInCity(
        int cid,
        CreateAirportDto body
    )
    {
        var response = new CityResponseService<GetAirportDto>();

        if (await AirportExists(body.Name))
        {
            response.Success = false;
            response.Message = "Airport already exists";
            return response;
        }

        var city = await _context.Cities.FirstOrDefaultAsync(c => c.Id == cid);

        if (city is null)
        {
            response.Success = false;
            response.Message = "City not found";
        }

        var newAirport = _mapper.Map<Ariport>(body);

        newAirport.CityId = cid;

        await _context.Ariports.AddAsync(newAirport);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetAirportDto>(newAirport);

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> CreateCommentInCity(
        int cid,
        CreateCommentDto body
    )
    {
        var response = new CityResponseService<GetCommetDto>();

        var newComment = _mapper.Map<Comment>(body);
        newComment.CityId = cid;
        newComment.UserId = getUserId();

        await _context.Comments.AddAsync(newComment);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCommetDto>(newComment);

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> UpdateCommentInCity(
        int cid,
        int cmid,
        UpdateCommnetDto body
    )
    {
        var response = new CityResponseService<GetCommetDto>();

        var comment = await _context.Comments.FirstOrDefaultAsync(
            c => (c.CityId == cid && c.Id == cmid)
        );

        if (comment == null)
        {
            response.Success = false;
            response.Message = "Comment not found";
        }

        comment!.Body = body.Body;
        comment.UpdatedAt = DateTime.Now;

        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCommetDto>(comment);

        return response;
    }

    public async Task<CityResponseService<GetCommetDto>> DeleteCommentInCity(int cid, int cmid)
    {
        var response = new CityResponseService<GetCommetDto>();

        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == cmid && c.CityId == cid);

        if (comment is null)
        {
            response.Success = false;
            response.Message = "Comment not found";
            return response;
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCommetDto>(comment);

        return response;
    }

    public async Task<CityResponseService<GetCityDto>> SearchCity(SearchCityDto body)
    {
        var response = new CityResponseService<GetCityDto>();

        var city = await _context.Cities.FirstOrDefaultAsync(
            c => c.Name.ToLower() == body.Name.ToLower()
        );

        if (city is null)
        {
            response.Success = false;
            response.Message = "City not found";
        }

        response.Data = _mapper.Map<GetCityDto>(city);

        return response;
    }

    public Task<CityResponseService<GetCityDto>> GetTravel()
    {
        throw new NotImplementedException();
    }

    public Task<CityResponseService<GetCityDto>> GetUpcomingTrips()
    {
        throw new NotImplementedException();
    }

    private async Task<bool> CityExists(string name)
    {
        return await _context.Cities.AnyAsync(c => c.Name == name.ToLower());
    }

    private async Task<bool> AirportExists(string name)
    {
        return await _context.Ariports.AnyAsync(a => a.Name.ToLower() == name.ToLower());
    }

    private int getUserId()
    {
        return int.Parse(
            _httpContextAccess.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}
