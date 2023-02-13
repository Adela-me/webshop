using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController : ApiController
{
    [HttpGet]
    public async Task<ActionResult> List()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        //  throw new Exception("Testing");
        return Ok(new { id });
    }
}
