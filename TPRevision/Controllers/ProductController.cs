using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TPRevision.Entities;

namespace API.Controller;

[ApiController]
[Route("api/product")]
public class ProductController(IProduitRepository repo) : ControllerBase
{
    // GET: api/product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produit>>> GetProduct()
    {
        return  Ok(await repo.GetAllAsync());
    }


    // GET api/product/2
    [HttpGet]
    [Route("[action]/{id}")]
    [ActionName("GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Produit>> GetProduct(int id)
    {
        var produit = await repo.GetByIdAsync(id);

        if (produit is null)
            return NotFound();

        return Ok(produit);
    }



    // POST api/product
    [HttpPost]
    public ActionResult<Produit> CreateProduct([FromBody] Produit product)
    {
        repo.AddAsync(product);
        repo.SaveAsync();

        return Ok(product);
    }

    // PATCH api/product
    [HttpPatch]
    public IActionResult ModifyProduct([FromBody] Produit product)
    {
        repo.UpdateAsync(product);
        return Ok(product);
    }

    // PUT api/product
    [HttpPut]
    public IActionResult ModifyProductPartially([FromBody] Produit product)
    {
        repo.UpdateAsync(product);
        return Ok(product);
    }

    // DELETE api/product
    [HttpDelete]
    public IActionResult ModifyProductPartially(int id)
    {
        repo.DeleteAsync(id);
        return NoContent();
    }
}