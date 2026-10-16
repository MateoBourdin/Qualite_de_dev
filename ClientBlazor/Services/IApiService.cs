namespace ClientBlazor.Services;

public interface IApiService<TListDto, TDetailDto>
    where TListDto : class
    where TDetailDto : class
{
    Task<IEnumerable<TListDto>> GetAllAsync();
    Task<TDetailDto?> GetByIdAsync(int id);
}
