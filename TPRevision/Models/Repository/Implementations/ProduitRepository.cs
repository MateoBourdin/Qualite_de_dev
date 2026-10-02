using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

public class ProduitRepository(DbProduitsContext context, AutoMapper.IConfigurationProvider mapperConfig)
    : IProduitRepository, IQueryService<ProduitDto, ProduitDetailDto>
{
    public async Task AddAsync(Produit entity)
    {
        await context.Produits.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await context.Produits.Where(p => p.IdProduit == id).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<Produit>> GetAllAsync()
    {
        return await context.Produits.ToListAsync();
    }

    public async Task<IEnumerable<Produit>> GetAllWithDetailsAsync()
    {
        return await context.Produits
            .Include(p => p.MarqueNavigation)
            .Include(p => p.TypeProduitNavigation)
            .ToListAsync();
    }

    public async Task<Produit?> GetByIdAsync(int id)
    {
        return await context.Produits.FirstOrDefaultAsync(p => p.IdProduit == id);
    }

    public async Task<Produit?> GetByIdWithDetailsAsync(int id)
    {
        return await context.Produits
            .Include(p => p.MarqueNavigation)
            .Include(p => p.TypeProduitNavigation)
            .FirstOrDefaultAsync(p => p.IdProduit == id);
    }

    public async Task<IEnumerable<Produit>> GetByMarqueAsync(int idMarque)
    {
        return await context.Produits.Where(p => p.IdMarque == idMarque).ToListAsync();
    }

    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Produit>> SearchByNameAsync(string term)
    {
        return await context.Produits.Where(p => p.NomProduit.Contains(term)).ToListAsync();
    }

    public async Task UpdateAsync(Produit entity)
    {
        await Task.Run(() => context.Produits.Update(entity));
    }

    public async Task<IEnumerable<ProduitDto>> GetAllDtoAsync()
    {
        return await context.Produits
            .ProjectTo<ProduitDto>(mapperConfig)
            .ToListAsync();
    }

    public async Task<ProduitDetailDto?> GetByIdDtoAsync(int id)
    {
        return await context.Produits
            .Where(p => p.IdProduit == id)
            .ProjectTo<ProduitDetailDto>(mapperConfig)
            .FirstOrDefaultAsync();
    }
}