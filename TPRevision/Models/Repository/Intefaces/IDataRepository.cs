public interface IDataRepository<T> : IReadable<T>, IWritable<T> where T : class
{
}