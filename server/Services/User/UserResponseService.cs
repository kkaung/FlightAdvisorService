namespace FlightAdvisorService.Services;

public class UserResponseService<T>
{
    public string Message { get; set; } = "";
    public T? Data;
    public bool Success { get; set; }
}
