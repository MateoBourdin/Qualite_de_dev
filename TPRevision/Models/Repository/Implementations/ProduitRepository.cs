
using Microsoft.EntityFrameworkCore;
using TPRevision.Entities;

public class ProduitRepository(DbProduitsContext context) : IProduitRepository
{
    public Task AddAsync(Produit entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Produit>> GetAllAsync()
    {
        return await context.Produits.ToListAsync();
    }

    public Task<IEnumerable<Produit>> GetAllWithDetailsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Produit?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Produit?> GetByIdWithDetailsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Produit>> GetByMarqueAsync(int idMarque)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Produit>> SearchByNameAsync(string term)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Produit entity)
    {
        throw new NotImplementedException();
    }
}

