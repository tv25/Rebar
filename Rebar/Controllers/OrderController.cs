using Microsoft.AspNetCore.Mvc;
using Rebar.Models;
using Rebar.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rebar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderServices;
        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orderes = await _orderServices.GetAllOrders();
            return Ok(orderes);
        }

       /* // GET api/OrderController/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
*/
        // POST api/OrderController
        [HttpPost]
        public async Task<IActionResult>Post (Order order)
        {
            await _orderServices.AddOrder(order);
            return Ok("created succesfuly");
        }

        

        /*  // PUT api/OrderController/5
          [HttpPut("{id}")]
          public async Task<IActionResult> Put(Guid id, [FromBody] Order neworder)
          {
              var order = await  _orderServices.GetById(id);
              if(order == null) { 
                  return NotFound();
              }

              await _orderServices.UpdateAsync(id, order);
              return Ok("update succesfuly");
          }*/

        /* // DELETE api/<OrderController>/5
         [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}
