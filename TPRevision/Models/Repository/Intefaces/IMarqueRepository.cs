using TPRevision.Entities;

public interface IMarqueRepository : IDataRepository<Marque>, IDetailReadable<Marque>, ISearchable<Marque>
{
}