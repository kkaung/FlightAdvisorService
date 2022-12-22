using Microsoft.EntityFrameworkCore;

namespace FlightAdvisorService.Services;

public class UserService : IUserService
{
    private DataContext _context;
    private IMapper _mapper;

    public UserService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponseService<List<GetUserDto>>> GetUsers()
    {
        var response = new UserResponseService<List<GetUserDto>>();

        response.Data = await _context.Users.Select(u => _mapper.Map<GetUserDto>(u)).ToListAsync();

        return response;
    }
}
