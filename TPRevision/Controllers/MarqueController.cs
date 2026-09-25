using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;


[ApiController]
[Route("[controller]")]
public class MarqueController(IDataRepository<Marque> repo, IMapper mapper) : ControllerBase
{
    // GET: api/marque
    [HttpGet]
    [Route("[action]")]
    [ActionName("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<MarqueDto>>> GetAllMarque()
    {
        var marques = await repo.GetAllAsync();
        return Ok(mapper.Map<IEnumerable<MarqueDto>>(marques));
    }

    // GET: api/marque
    [HttpGet]
    [Route("[action]")]
    [ActionName("GetAllWithDetails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Marque>>> GetAllMarqueWithDetails()
    {
        return Ok(await repo.GetAllWithDetailsAsync());
    }



    // GET api/marque/2
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MarqueDto>> GetMarqueById(int id)
    {
        var tproduit = await repo.GetByIdAsync(id);

        if (tproduit is null)
            return NotFound();

        return Ok(mapper.Map<MarqueDto>(tproduit));
    }

    // GET api/marque/2
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetByIdWithDetails")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Marque>> GetMarqueByIdWithDetails(int id)
    {
        var tproduit = await repo.GetByIdWithDetailsAsync(id);

        if (tproduit is null)
            return NotFound();

        return Ok(tproduit);
    }

    // POST api/product
    [HttpPost]
    [Route("[action]")]
    [ActionName("AddProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Produit>> PostMarque([FromBody] Marque marque)
    {
        await repo.AddAsync(marque);
        await repo.SaveAsync();

        return CreatedAtAction("GetById", new { id = marque.IdMarque }, marque);
    }

    // PATCH api/product
    [HttpPatch]
    [Route("[action]")]
    [ActionName("ModifyProduct")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ModifyMarque([FromBody] Marque marque)
    {
        repo.UpdateAsync(marque);
        return Ok(marque);
    }

    // PUT api/product
    [HttpPut]
    [Route("[action]")]
    [ActionName("ModifyProductPartially")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ModifyMarquePartially([FromBody] Marque marque)
    {
        repo.UpdateAsync(marque);
        return Ok(marque);
    }

    // DELETE api/product
    [HttpDelete]
    [Route("[action]/{id}")]
    [ActionName("Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ModifyMarquePartially(int id)
    {
        repo.DeleteAsync(id);
        return NoContent();
    }
}

