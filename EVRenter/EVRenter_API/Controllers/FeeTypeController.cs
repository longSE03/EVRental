using EVRenter_Data.Entities;
using EVRenter_Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVRenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeTypeController : ControllerBase
    {
        private readonly FeeTypeService _feeTypeService;

        public FeeTypeController(FeeTypeService feeTypeService)
        {
            _feeTypeService = feeTypeService;
        }

        // GET: api/FeeType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeeType>>> GetAll()
        {
            var list = await _feeTypeService.GetAllAsync();
            return Ok(list);
        }

        // GET: api/FeeType/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeeType>> GetById(int id)
        {
            var item = await _feeTypeService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { message = $"FeeType with ID {id} not found" });

            return Ok(item);
        }

        // GET: api/FeeType/by-extrafee/{extraFeeId}
        [HttpGet("by-extrafee/{extraFeeId}")]
        public async Task<ActionResult<IEnumerable<FeeType>>> GetByExtraFee(int extraFeeId)
        {
            var list = await _feeTypeService.GetByExtraFeeIdAsync(extraFeeId);
            return Ok(list);
        }

        // POST: api/FeeType
        [HttpPost]
        public async Task<ActionResult<FeeType>> Create(FeeType feeType)
        {
            var created = await _feeTypeService.CreateAsync(feeType);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/FeeType/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FeeType feeType)
        {
            if (id != feeType.Id)
                return BadRequest(new { message = "ID in route does not match entity ID" });

            var success = await _feeTypeService.UpdateAsync(feeType);
            if (!success)
                return NotFound(new { message = $"FeeType with ID {id} not found" });

            return NoContent();
        }

        // DELETE: api/FeeType/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _feeTypeService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"FeeType with ID {id} not found" });

            return NoContent();
        }
    }
}
