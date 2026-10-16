using ClientBlazor.Dtos;

namespace ClientBlazor.Services;

public class MarqueService(HttpClient http) : ApiService<MarqueDto, MarqueDto>(http)
{
    protected override string Route => "api/marque";
}
