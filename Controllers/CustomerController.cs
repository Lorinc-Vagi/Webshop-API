using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Webshop_API.Context;
using Webshop_API.Entities;

namespace Webshop_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly WebshopContext context;

        public CustomerController(WebshopContext webshopContext)
        {
            context = webshopContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            return await context.Customers.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var toDisp = await context.Customers.FindAsync(id);
            if (toDisp == null)
            {
                return NotFound();
            }
            return toDisp;
        }
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            context.Customers.AddAsync(customer);
            context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerById(int id, [FromBody]Customer customer) 
        {
            if (id !=customer.Id)
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
            var toDel = await context.Customers.FindAsync(id);
            if (toDel == null)
            {
                return NotFound();
            }
            context.Customers.Remove(toDel);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
