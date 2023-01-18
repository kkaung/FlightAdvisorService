namespace FlightAdvisorService.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public Role Role { get; set; } = Role.User;
    public byte[]? PasswordSalt { get; set; }
    public byte[]? PasswordHash { get; set; }
}

public enum Role
{
    Admin,
    User
}
