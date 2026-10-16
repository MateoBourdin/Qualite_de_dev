using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

public class TypeProduitRepository(DbProduitsContext context, AutoMapper.IConfigurationProvider mapperConfig)
    : ITypeProduitRepository, IQueryService<TypeProduitDto, TypeProduitDto>
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

    public Task UpdateAsync(TypeProduit entity)
    {
        // Update ne fait que marquer l'entité comme modifiée : l'écriture se fait au SaveAsync
        context.TypeProduits.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<TypeProduitDto>> GetAllDtoAsync()
    {
        return await context.TypeProduits
            .ProjectTo<TypeProduitDto>(mapperConfig)
            .ToListAsync();
    }

    public async Task<TypeProduitDto?> GetByIdDtoAsync(int id)
    {
        return await context.TypeProduits
            .Where(p => p.IdTypeProduit == id)
            .ProjectTo<TypeProduitDto>(mapperConfig)
            .FirstOrDefaultAsync();
    }
}
