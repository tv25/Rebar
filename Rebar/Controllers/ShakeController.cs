using Microsoft.AspNetCore.Mvc;
using Rebar.Models;
using Rebar.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rebar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShakeController : ControllerBase
    {
        private readonly IShakeServices _shakeServices;
        public ShakeController(IShakeServices shakeServices)
        {
            _shakeServices = shakeServices;
        }
        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shakes = await _shakeServices.GetAllShakes();
            return Ok(shakes);
        }

       /* // GET api/OrderController/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/OrderController
        [HttpPost]
        public async Task<IActionResult>Post (Shake shake)
        {
            await _shakeServices.AddShake(shake);
            return Ok("created succesfuly");
        }
     

       /* // PUT api/OrderController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Shake newshake)
        {
            var shake = await  _shakeServices.GetById(id);
            if(shake == null) { 
                return NotFound();
            }

            await _shakeServices.UpdateAsync(id, shake);
            return Ok("update succesfuly");
        }*/

       /* // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
