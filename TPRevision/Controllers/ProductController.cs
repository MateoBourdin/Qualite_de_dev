using Microsoft.AspNetCore.Mvc;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

[Route("[controller]")]
public class ProductController(IProduitRepository repo, IQueryService<ProduitDto, ProduitDetailDto> queryService)
    : CrudController<Produit, ProduitDto, ProduitDetailDto>(repo, repo, queryService)
{
    protected override int GetId(Produit entity) => entity.IdProduit;
}
