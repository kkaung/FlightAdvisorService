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

    public async Task<ServiceResponse<List<GetCityDto>>> GetCities()
    {
        var response = new ServiceResponse<List<GetCityDto>>();

        var cities = await _context.Cities
            .Include(c => c.Airports)
            .Include(c => c.Comments)
            .ToListAsync();

        response.Data = cities.Select(c => _mapper.Map<GetCityDto>(c)).ToList();

        return response;
    }

    public async Task<ServiceResponse<GetCityDto>> CreateCity(CreateCityDto body)
    {
        var response = new ServiceResponse<GetCityDto>();

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

    public async Task<ServiceResponse<List<GetAirportDto>>> GetAirportsInCity(string byName)
    {
        var response = new ServiceResponse<List<GetAirportDto>>();

        var city = await _context.Cities.FirstOrDefaultAsync(
            c => c.Name.ToLower().Contains(byName.ToLower())
        );

        if (city is null)
        {
            response.Message = "City not found!";
            response.Success = false;
            return response;
        }

        var airports = await _context.Ariports.Where(c => c.CityId == c.CityId).ToListAsync();

        response.Data = airports.Select(c => _mapper.Map<GetAirportDto>(c)).ToList();

        return response;
    }

    public async Task<ServiceResponse<GetAirportDto>> CreateAirportInCity(
        int cid,
        CreateAirportDto body
    )
    {
        var response = new ServiceResponse<GetAirportDto>();

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

    public async Task<ServiceResponse<GetCommetDto>> CreateCommentInCity(
        int cid,
        CreateCommentDto body
    )
    {
        var response = new ServiceResponse<GetCommetDto>();

        var newComment = _mapper.Map<Comment>(body);
        newComment.CityId = cid;
        newComment.UserId = GetUserId();

        await _context.Comments.AddAsync(newComment);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetCommetDto>(newComment);

        return response;
    }

    public async Task<ServiceResponse<GetCommetDto>> UpdateCommentInCity(
        int cid,
        int cmid,
        UpdateCommnetDto body
    )
    {
        var response = new ServiceResponse<GetCommetDto>();

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

    public async Task<ServiceResponse<GetCommetDto>> DeleteCommentInCity(int cid, int cmid)
    {
        var response = new ServiceResponse<GetCommetDto>();

        var comment = await _context.Comments.FirstOrDefaultAsync(
            c => c.Id == cmid && c.CityId == cid
        );

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

    public async Task<ServiceResponse<List<GetCityDto>>> SearchCity(SearchCityDto body, int cLimit)
    {
        var response = new ServiceResponse<List<GetCityDto>>();

        var cities = await _context.Cities
            .Include(c => c.Airports)
            .Include(c => c.Comments)
            .Where(c => c.Name.ToLower().Contains(body.ByName.ToLower()))
            .ToListAsync();

        response.Data = cities.Select(c => _mapper.Map<GetCityDto>(c)).ToList();

        return response;
    }

    public async Task<ServiceResponse<GetTripDto>> CreateTrip(CreateTripDto body)
    {
        var response = new ServiceResponse<GetTripDto>();

        var start = await FindAirport(body.Start);
        var end = await FindAirport(body.End);
        var through = new List<Ariport>() { };
        var throughAirportIds = new List<int>() { };

        if (start is null || end is null)
        {
            response.Success = false;
            response.Message = "Airport not found";
            return response;
        }

        foreach (var a in body.Through)
        {
            var airport = await FindAirport(a);

            if (airport is not null)
            {
                throughAirportIds.Add(airport!.Id);
                through.Add(airport);
            }
        }

        var newTrip = new Trip();

        newTrip.StartAriportId = start.Id;
        newTrip.EndAirportId = end.Id;
        newTrip.ThroughAirport = through;
        newTrip.TotalPrice = body.price;
        newTrip.TotalDistance = Haversine.Distance(
            start.Latitude,
            start.Longitude,
            end.Latitude,
            end.Longitude
        );

        await _context.Trips.AddAsync(newTrip);
        await _context.SaveChangesAsync();

        var trip = _mapper.Map<GetTripDto>(newTrip);

        trip.Price.Total = newTrip.TotalPrice;
        trip.Distance.Total = newTrip.TotalDistance;
        trip.Start = _mapper.Map<GetAirportDto>(start);
        trip.End = _mapper.Map<GetAirportDto>(end);
        trip.Through = through.Select(a => _mapper.Map<GetAirportDto>(a)).ToList();

        response.Data = trip;

        return response;
    }

    public async Task<ServiceResponse<List<GetTripDto>>> GetTrips(string from, string to)
    {
        var response = new ServiceResponse<List<GetTripDto>>();

        if (from == "")
        {
            Console.WriteLine("NULL");
        }

        var fromCity = await FindCity(from);
        var toCity = await FindCity(to);

        var fromAirportIds = fromCity.Airports!.Select(a => a.Id).ToList();
        var toAirportIds = toCity.Airports!.Select(a => a.Id).ToList();

        var FoundTrips = new List<GetTripDto>();

        foreach (var fid in fromAirportIds)
        {
            foreach (var tid in toAirportIds)
            {
                var trip = await _context.Trips
                    .Include(t => t.ThroughAirport)
                    .FirstOrDefaultAsync(t => t.StartAriportId == fid && t.EndAirportId == tid);

                if (trip is not null)
                {
                    var mapTrip = _mapper.Map<GetTripDto>(trip);

                    mapTrip.Distance.Total = trip.TotalDistance;
                    mapTrip.Price.Total = trip.TotalPrice;
                    mapTrip.Start = _mapper.Map<GetAirportDto>(
                        await _context.Ariports.FirstAsync(a => a.Id == trip.StartAriportId)
                    );
                    mapTrip.End = _mapper.Map<GetAirportDto>(
                        await _context.Ariports.FirstAsync(a => a.Id == trip.EndAirportId)
                    );
                    mapTrip.Through = trip.ThroughAirport
                        .Select(a => _mapper.Map<GetAirportDto>(a))
                        .ToList();

                    FoundTrips.Add(mapTrip);
                }
            }
        }

        response.Data = FoundTrips;

        return response;
    }

    public Task<ServiceResponse<GetCityDto>> GetUpcomingTrips()
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

    private async Task<Ariport> FindAirport(string name)
    {
        return await _context.Ariports.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
    }

    private async Task<City> FindCity(string name)
    {
        return await _context.Cities
            .Include(c => c.Airports)
            .FirstOrDefaultAsync(c => c.Name.ToLower() == name.ToLower());
    }

    private int GetUserId()
    {
        return int.Parse(
            _httpContextAccess.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}
