using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace FlightAdvisorService.Services;

public class AuthService : IAuthService
{
    private DataContext _context;
    private readonly IMapper _mapper;
    private IConfiguration _configuration;

    public AuthService(DataContext dataContext, IMapper mapper, IConfiguration configuration)
    {
        _context = dataContext;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task<AuthResponseService<ResponseRegisterDto>> Register(RegisterDto body)
    {
        var response = new AuthResponseService<ResponseRegisterDto>();

        // Check if user already registered
        if (await UserExists(body.Email))
        {
            response.Success = false;
            response.Message = "User already registered";
            return response;
        }

        var newUser = _mapper.Map<User>(body);

        // Hash the password
        HashPassword(body.Password, out byte[] passwordSalt, out byte[] passwordHash);

        newUser.PasswordSalt = passwordSalt;
        newUser.PasswordHash = passwordHash;

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        response.Data = _mapper.Map<ResponseRegisterDto>(newUser);

        return response;
    }

    public async Task<AuthResponseService<string>> Login(LoginDtos body)
    {
        var response = new AuthResponseService<string>();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(body.Email));

        if (user is null)
        {
            response.Success = false;
            response.Message = "Invalid credentials";
        }
        else if (!ValidatePassword(body.Password, user.PasswordSalt, user.PasswordHash))
        {
            response.Success = false;
            response.Message = "Invalid credentials";
        }
        else
        {
            response.Data = CreateJwtToken(user);
        }

        return response;
    }

    private async Task<Boolean> UserExists(string email)
    {
        var user = await _context.Users.AnyAsync(u => u.Email == email);
        if (user)
            return true;

        return false;
    }

    private void HashPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    private Boolean ValidatePassword(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computeHash.SequenceEqual(passwordHash);
        }
    }

    private string CreateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email)
        };
        var appSettingToken = _configuration.GetSection("AppSettings:Token").Value;

        if (appSettingToken is null)
            throw new Exception("Appsetting Token is null");

        SymmetricSecurityKey key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(appSettingToken)
        );

        SigningCredentials creds = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha512Signature
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
