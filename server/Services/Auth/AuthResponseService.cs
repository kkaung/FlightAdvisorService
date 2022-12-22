namespace FlightAdvisorService.Services;

public class AuthResponseService<T>
{
    public Boolean Success = true;
    public T? Data;
    public String Message = "";
}
