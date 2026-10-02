public interface ISearchable<T> where T : class
{
    Task<IEnumerable<T>> SearchByNameAsync(string term);
}