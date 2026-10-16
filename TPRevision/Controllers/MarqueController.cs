using Microsoft.AspNetCore.Mvc;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

[Route("api/marque")]
public class MarqueController(IMarqueRepository repo, IQueryService<MarqueDto, MarqueDto> queryService)
    : CrudController<Marque, MarqueDto, MarqueDto>(repo, repo, queryService)
{
    protected override int GetId(Marque entity) => entity.IdMarque;
}
