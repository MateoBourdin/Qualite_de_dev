using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class CrudController<TEntity, TListDto, TDetailDto>(
    IDataRepository<TEntity> repo,
    IDetailReadable<TEntity> detailReader,
    IQueryService<TListDto, TDetailDto> queryService) : ControllerBase
    where TEntity : class
    where TListDto : class
    where TDetailDto : class
{
    // Chaque contrôleur donne l'id de son entité (utilisé pour le CreatedAtAction du POST)
    protected abstract int GetId(TEntity entity);

    [HttpGet("[action]")]
    [ActionName("GetAll")]
    public async Task<ActionResult<IEnumerable<TListDto>>> GetAll()
    {
        return Ok(await queryService.GetAllDtoAsync());
    }

    [HttpGet("[action]/{id}")]
    [ActionName("GetById")]
    public async Task<ActionResult<TDetailDto>> GetById(int id)
    {
        var dto = await queryService.GetByIdDtoAsync(id);

        if (dto is null)
            return NotFound();

        return Ok(dto);
    }

    [HttpGet("[action]")]
    [ActionName("GetAllWithDetails")]
    public async Task<ActionResult<IEnumerable<TEntity>>> GetAllWithDetails()
    {
        return Ok(await detailReader.GetAllWithDetailsAsync());
    }

    [HttpGet("[action]/{id}")]
    [ActionName("GetByIdWithDetails")]
    public async Task<ActionResult<TEntity>> GetByIdWithDetails(int id)
    {
        var entity = await detailReader.GetByIdWithDetailsAsync(id);

        if (entity is null)
            return NotFound();

        return Ok(entity);
    }

    // Les écritures se font avec l'entité complète, les DTOs servent uniquement à l'affichage
    [HttpPost("[action]")]
    [ActionName("AddProduct")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<TEntity>> Post([FromBody] TEntity entity)
    {
        await repo.AddAsync(entity);
        await repo.SaveAsync();

        return CreatedAtAction("GetById", new { id = GetId(entity) }, entity);
    }

    [HttpPatch("[action]")]
    [ActionName("ModifyProduct")]
    public async Task<IActionResult> Patch([FromBody] TEntity entity)
    {
        return await Update(entity);
    }

    [HttpPut("[action]")]
    [ActionName("ModifyProductPartially")]
    public async Task<IActionResult> Put([FromBody] TEntity entity)
    {
        return await Update(entity);
    }

    [HttpDelete("[action]/{id}")]
    [ActionName("Delete")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await repo.DeleteAsync(id);
        }
        catch (DbException)
        {
            // La base refuse la suppression (ex : une marque encore utilisée par des produits)
            return Conflict("Impossible de supprimer : l'élément est encore référencé.");
        }

        return NoContent();
    }

    private async Task<IActionResult> Update(TEntity entity)
    {
        try
        {
            await repo.UpdateAsync(entity);
            await repo.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // Aucune ligne modifiée : l'id envoyé n'existe pas
            return NotFound();
        }

        return Ok(entity);
    }
}
