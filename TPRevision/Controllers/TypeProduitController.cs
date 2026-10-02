using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;


[ApiController]
[Route("[controller]")]
public class TypeProduitController(ITypeProduitRepository repo, IQueryService<TypeProduitDto, TypeProduitDto> queryService) : ControllerBase
{
    // GET: api/typeproduit
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
    public async Task<ActionResult<IEnumerable<TypeProduitDto>>> GetAllProduct()
    {
        return Ok(await queryService.GetAllDtoAsync());
    }

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
    public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAllProductWithDetails()
    {
        return Ok(await repo.GetAllWithDetailsAsync());
    }


    // GET api/typeproduit/2
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
    public async Task<ActionResult<TypeProduitDto>> GetProductById(int id)
    {
        var tproduit = await queryService.GetByIdDtoAsync(id);

        if (tproduit is null)
            return NotFound();

        return Ok(tproduit);
    }

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
    public async Task<ActionResult<TypeProduit>> GetProductByIdWithDetails(int id)
    {
        var tproduit = await repo.GetByIdWithDetailsAsync(id);

        if (tproduit is null)
            return NotFound();

        return Ok(tproduit);
    }


    // POST api/typeproduit
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
    public async Task<ActionResult<TypeProduit>> PostProduct([FromBody] TypeProduit tproduct)
    {
        await repo.AddAsync(tproduct);
        await repo.SaveAsync();

        return CreatedAtAction("GetById", new { id = tproduct.IdTypeProduit }, tproduct);
    }

    // PATCH api/typeproduit
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
    public IActionResult ModifyProduct([FromBody] TypeProduit tproduct)
    {
        repo.UpdateAsync(tproduct);
        return Ok(tproduct);
    }

    // PUT api/typeproduit
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
    public IActionResult ModifyProductPartially([FromBody] TypeProduit tproduct)
    {
        repo.UpdateAsync(tproduct);
        return Ok(tproduct);
    }

    // DELETE api/typeproduit
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
    public IActionResult ModifyProductPartially(int id)
    {
        repo.DeleteAsync(id);
        return NoContent();
    }
}