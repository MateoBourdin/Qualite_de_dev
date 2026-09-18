
using Microsoft.EntityFrameworkCore;
using TPRevision.Entities;

public class MarqueRepository(DbProduitsContext context) : IDataRepository<Marque>
{
    public async Task AddAsync(Marque entity)
    {
        await context.Marques.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await context.Marques.Where(p => p.IdMarque == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Marque>> GetAllAsync()
    {
        return await context.Marques.ToListAsync();
    }

    public async Task<IEnumerable<Marque>> GetAllWithDetailsAsync()
    {
        return await context.Marques.Include(p => p.produits).ToListAsync();
    }

    public async Task<Marque?> GetByIdAsync(int id)
    {
        return await context.Marques.FirstOrDefaultAsync(p => p.IdMarque == id);
    }

    public async Task<Marque?> GetByIdWithDetailsAsync(int id)
    {
        return await context.Marques.Include(p => p.produits).FirstOrDefaultAsync(p => p.IdMarque == id);
    }

    public async Task<IEnumerable<Marque>> GetByMarqueAsync(int idMarque)
    {
        return await context.Marques.Where(p => p.IdMarque == idMarque).ToListAsync();
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Marque>> SearchByNameAsync(string term)
    {
        return await context.Marques.Where(p => p.NomMarque.Contains(term)).ToListAsync();
    }

    public async Task UpdateAsync(Marque entity)
    {
        await Task.Run(() => context.Marques.Update(entity));
    }
}

