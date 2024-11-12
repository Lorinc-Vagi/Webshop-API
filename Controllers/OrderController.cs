using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop_API.Context;
using Webshop_API.Entities;

namespace Webshop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly WebshopContext context;

        public OrderController(WebshopContext webshopContext)
        {
            context = webshopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllCustomers()
        {
            return await context.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetCustomerById(int id)
        {
            var toDisp = await context.Orders.FindAsync(id);
            if (toDisp == null)
            {
                return NotFound();
            }
            return toDisp;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateCustomer([FromBody] Order customer)
        {
            context.Orders.AddAsync(customer);
            context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerById(int id, [FromBody] Order customer)
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
            var toDel = await context.Orders.FindAsync(id);
            if (toDel == null)
            {
                return NotFound();
            }
            context.Orders.Remove(toDel);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
