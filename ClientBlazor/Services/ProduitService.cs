using ClientBlazor.Dtos;

namespace ClientBlazor.Services;

public class ProduitService(HttpClient http) : ApiService<ProduitDto, ProduitDetailDto>(http)
{
    protected override string Route => "Product";
}
