using EVRenter_Data.Entities;
using EVRenter_Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVRenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtraFeeController : ControllerBase
    {
        private readonly ExtraFeeService _extraFeeService;

        public ExtraFeeController(ExtraFeeService extraFeeService)
        {
            _extraFeeService = extraFeeService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExtraFee>>> GetAll()
        {
            var list = await _extraFeeService.GetAllAsync();
            return Ok(list);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ExtraFee>> GetById(int id)
        {
            var item = await _extraFeeService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { message = $"ExtraFee with ID {id} not found" });

            return Ok(item);
        }

        
        [HttpPost]
        public async Task<ActionResult<ExtraFee>> Create(ExtraFee extraFee)
        {
            var created = await _extraFeeService.CreateAsync(extraFee);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExtraFee extraFee)
        {
            if (id != extraFee.Id)
                return BadRequest(new { message = "ID in route does not match entity ID" });

            var success = await _extraFeeService.UpdateAsync(extraFee);
            if (!success)
                return NotFound(new { message = $"ExtraFee with ID {id} not found" });

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _extraFeeService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"ExtraFee with ID {id} not found" });

            return NoContent();
        }
    }
}
