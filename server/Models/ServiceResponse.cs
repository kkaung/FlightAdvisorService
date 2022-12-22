namespace FlightAdvisorService.Models;

public class ServiceResponse<T>
{
    public Boolean Success = true;
    public String Message = "";
    public T? Data;
}
