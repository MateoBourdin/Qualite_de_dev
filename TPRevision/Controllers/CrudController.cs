


using Microsoft.AspNetCore.Mvc;
using TPRevision.Models.Model.Dtos;

public class CrudController<T>: ControllerBase where T : class 
{

    public async Task<ActionResult<IEnumerable<MarqueDto>>> GetAll()
    {
        return Ok(await queryService.GetAllDtoAsync());
    }

    public async Task<ActionResult<IEnumerable<Marque>>> GetAllWithDetails()
    {
        return Ok(await repo.GetAllWithDetailsAsync());
    }
    public async Task<ActionResult<MarqueDto>> GetById(int id)
    {
        var elt = await queryService.GetByIdDtoAsync(id);

        if (elt is null)
            return NotFound();

        return Ok(elt);
    }


    public async Task<ActionResult<Marque>> GetByIdWithDetails(int id)
    {
        var elt = await repo.GetByIdWithDetailsAsync(id);

        if (elt is null)
            return NotFound();

        return Ok(elt);
    }


    public async Task<ActionResult<Marque>> Post([FromBody] Marque marque)
    {
        await repo.AddAsync(marque);
        await repo.SaveAsync();

        return CreatedAtAction("GetById", new { id = marque.IdMarque }, marque);
    }


    public IActionResult Modify([FromBody] Marque marque)
    {
        repo.UpdateAsync(marque);
        return Ok(marque);
    }

}