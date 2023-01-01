namespace FlightAdvisorService.DTOs;

public class GetUserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public Role Role { get; set; } = Role.User;
}
