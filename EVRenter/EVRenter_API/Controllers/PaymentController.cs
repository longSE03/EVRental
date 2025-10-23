using EVRenter_Data.Entities;
using EVRenter_Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVRenter_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAll()
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        // GET: api/Payment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null)
                return NotFound(new { message = $"Payment with ID {id} not found" });

            return Ok(payment);
        }

        // GET: api/Payment/by-booking/{bookingId}
        [HttpGet("by-booking/{bookingId}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetByBookingId(int bookingId)
        {
            var payments = await _paymentService.GetByBookingIdAsync(bookingId);
            return Ok(payments);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<Payment>> Create(Payment payment)
        {
            var created = await _paymentService.CreateAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Payment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Payment payment)
        {
            if (id != payment.Id)
                return BadRequest(new { message = "ID in route does not match entity ID" });

            var success = await _paymentService.UpdateAsync(payment);
            if (!success)
                return NotFound(new { message = $"Payment with ID {id} not found" });

            return NoContent();
        }

        // DELETE: api/Payment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _paymentService.DeleteAsync(id);
            if (!success)
                return NotFound(new { message = $"Payment with ID {id} not found" });

            return NoContent();
        }
    }
}
