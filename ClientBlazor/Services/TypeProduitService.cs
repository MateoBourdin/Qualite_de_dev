using ClientBlazor.Dtos;

namespace ClientBlazor.Services;

public class TypeProduitService(HttpClient http) : ApiService<TypeProduitDto, TypeProduitDto>(http)
{
    protected override string Route => "TypeProduit";
}
