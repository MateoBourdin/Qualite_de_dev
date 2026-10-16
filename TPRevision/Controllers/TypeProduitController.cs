using Microsoft.AspNetCore.Mvc;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

[Route("[controller]")]
public class TypeProduitController(ITypeProduitRepository repo, IQueryService<TypeProduitDto, TypeProduitDto> queryService)
    : CrudController<TypeProduit, TypeProduitDto, TypeProduitDto>(repo, repo, queryService)
{
    protected override int GetId(TypeProduit entity) => entity.IdTypeProduit;
}
