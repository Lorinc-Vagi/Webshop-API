using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop_API.Context;
using Webshop_API.Entities;

namespace Webshop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly WebshopContext context;

        public ProductController(WebshopContext webshopContext)
        {
            context = webshopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllCustomers()
        {
            return await context.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetCustomerById(int id)
        {
            var toDisp = await context.Products.FindAsync(id);
            if (toDisp == null)
            {
                return NotFound();
            }
            return toDisp;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateCustomer([FromBody] Product customer)
        {
            context.Products.AddAsync(customer);
            context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerById(int id, [FromBody] Product customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }
            context.Entry(customer).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var toDel = await context.Products.FindAsync(id);
            if (toDel == null)
            {
                return NotFound();
            }
            context.Products.Remove(toDel);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
