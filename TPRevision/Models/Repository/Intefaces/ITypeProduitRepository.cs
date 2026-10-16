using TPRevision.Entities;

public interface ITypeProduitRepository : IDataRepository<TypeProduit>, IDetailReadable<TypeProduit>, ISearchable<TypeProduit>
{
}
