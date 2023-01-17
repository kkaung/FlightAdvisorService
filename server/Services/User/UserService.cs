using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Services;

public class UserService : IUserService
{
    private DataContext _context;
    private IMapper _mapper;
    private Hash _hash;
    private IHttpContextAccessor _httpContextAccessor;

    public UserService(
        DataContext context,
        IMapper mapper,
        Hash hash,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _context = context;
        _mapper = mapper;
        _hash = hash;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ServiceResponse<GetUserDto>> GetMe()
    {
        var response = new ServiceResponse<GetUserDto>();

        var uid = GetUserId();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == uid);

        if (user is null)
        {
            response.Success = false;
            response.Message = "Invalid token";
        }

        response.Data = _mapper.Map<GetUserDto>(user);

        return response;
    }

    public async Task<ServiceResponse<List<GetUserDto>>> GetUsers()
    {
        var response = new ServiceResponse<List<GetUserDto>>();

        response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();

        return response;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUser(int id)
    {
        var response = new ServiceResponse<GetUserDto>();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            response.Success = false;
            response.Message = "User not found!";
            return response;
        }

        response.Data = _mapper.Map<GetUserDto>(user);

        return response;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto body)
    {
        var response = new ServiceResponse<GetUserDto>();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            response.Success = false;
            response.Message = "User not found!";
            return response;
        }

        user.FirstName = body.FirstName;
        user.LastName = body.LastName;
        user.Email = body.Email;

        if (body.Password != "")
        {
            _hash.HashPassword(body.Password, out byte[] passwordSalt, out byte[] passwordHash);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
        }

        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<GetUserDto>(user);

        return response;
    }

    public async Task<ServiceResponse<int>> DeleteUser(int id)
    {
        var response = new ServiceResponse<int>();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        if (user is null)
        {
            response.Success = false;
            response.Message = "User not found!";
            return response;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        response.Data = id;

        return response;
    }

    private int GetUserId()
    {
        return Convert.ToInt16(
            _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
    }
}
