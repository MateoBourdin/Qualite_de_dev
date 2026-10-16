using TPRevision.Entities;

public interface IProduitRepository : IDataRepository<Produit>, IDetailReadable<Produit>, ISearchable<Produit>
{
    Task<IEnumerable<Produit>> GetByMarqueAsync(int idMarque);
}
