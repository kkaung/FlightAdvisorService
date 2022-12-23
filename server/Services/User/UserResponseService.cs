namespace FlightAdvisorService.Services;

public class UserResponseService<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; } = String.Empty;
    public bool Success { get; set; } = true;
}
