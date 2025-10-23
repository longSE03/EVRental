using EVRenter_Data.Entities;
using EVRenter_Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVRenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandoverAndReturnController : ControllerBase
    {
        private readonly HandoverAndReturnService _handoverService;

        public HandoverAndReturnController(HandoverAndReturnService handoverService)
        {
            _handoverService = handoverService;
        }

        // GET: api/HandoverAndReturn
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HandoverAndReturn>>> GetAll()
        {
            var list = await _handoverService.GetAllAsync();
            return Ok(list);
        }

        // GET: api/HandoverAndReturn/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HandoverAndReturn>> GetById(int id)
        {
            var item = await _handoverService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { message = $"HandoverAndReturn with ID {id} not found" });

            return Ok(item);
        }

        // GET: api/HandoverAndReturn/by-booking/{bookingId}
        [HttpGet("by-booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<HandoverAndReturn>>> GetByBookingId(int bookingId)
        {
            var list = await _handoverService.GetByBookingIdAsync(bookingId);
            return Ok(list);
        }

        // POST: api/HandoverAndReturn
        [HttpPost]
        public async Task<ActionResult<HandoverAndReturn>> Create(HandoverAndReturn entity)
        {
            var created = await _handoverService.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/HandoverAndReturn/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, HandoverAndReturn entity)
        {
            if (id != entity.Id)
                return BadRequest(new { message = "ID in route does not match entity ID" });

            var success = await _handoverService.UpdateAsync(entity);
            if (!success)
                return NotFound(new { message = $"HandoverAndReturn with ID {id} not found" });

            return NoContent();
        }

        // DELETE: api/HandoverAndReturn/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _handoverService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"HandoverAndReturn with ID {id} not found" });

            return NoContent();
        }
    }
}
