using TPRevision.Entities;

public interface IDataRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithDetailsAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetByIdWithDetailsAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}