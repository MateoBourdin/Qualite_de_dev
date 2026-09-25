
using Microsoft.EntityFrameworkCore;
using TPRevision.Entities;

public class TypeProduitRepository(DbProduitsContext context) : IDataRepository<TypeProduit>
{
    public async Task AddAsync(TypeProduit entity)
    {
        await context.TypeProduits.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await context.TypeProduits.Where(p => p.IdTypeProduit == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<TypeProduit>> GetAllAsync()
    {
        return await context.TypeProduits.ToListAsync();
    }

    public async Task<IEnumerable<TypeProduit>> GetAllWithDetailsAsync()
    {
        return await context.TypeProduits.Include(p => p.produits).ToListAsync();
    }

    public async Task<TypeProduit?> GetByIdAsync(int id)
    {
        return await context.TypeProduits.FirstOrDefaultAsync(p => p.IdTypeProduit == id);
    }

    public async Task<TypeProduit?> GetByIdWithDetailsAsync(int id)
    {
        return await context.TypeProduits.Include(p => p.produits).FirstOrDefaultAsync(p => p.IdTypeProduit == id);
    }

   

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TypeProduit>> SearchByNameAsync(string term)
    {
        return await context.TypeProduits.Where(p => p.NomTypeProduit.Contains(term)).ToListAsync();
    }

    public async Task UpdateAsync(TypeProduit entity)
    {
        await Task.Run(() => context.TypeProduits.Update(entity));
    }
}

