public interface IDetailReadable<T> where T : class
{
    Task<IEnumerable<T>> GetAllWithDetailsAsync();
    Task<T?> GetByIdWithDetailsAsync(int id);
}
