namespace FlightAdvisorService.Helpers;

public class Hash
{
    public void HashPassword(string password, out byte[] passwordSalt, out byte[] passwordHash)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
