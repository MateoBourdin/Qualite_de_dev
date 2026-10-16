using System.Net.Http.Json;

namespace ClientBlazor.Services;

// Même principe que le CrudController de l'API : la logique HTTP est écrite une fois,
// chaque service ne donne que la route de son contrôleur
public abstract class ApiService<TListDto, TDetailDto>(HttpClient http) : IApiService<TListDto, TDetailDto>
    where TListDto : class
    where TDetailDto : class
{
    protected abstract string Route { get; }

    public async Task<IEnumerable<TListDto>> GetAllAsync()
    {
        var result = await http.GetFromJsonAsync<IEnumerable<TListDto>>($"{Route}/GetAll");
        return result ?? [];
    }

    public async Task<TDetailDto?> GetByIdAsync(int id)
    {
        var response = await http.GetAsync($"{Route}/GetById/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<TDetailDto>();
    }
}
