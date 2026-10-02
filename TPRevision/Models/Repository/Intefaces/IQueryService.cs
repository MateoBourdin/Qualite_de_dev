public interface IQueryService<TListDto, TDetailDto>
    where TListDto : class
    where TDetailDto : class
{
    Task<IEnumerable<TListDto>> GetAllDtoAsync();
    Task<TDetailDto?> GetByIdDtoAsync(int id);
}