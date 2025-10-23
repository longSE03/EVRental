using EVRenter_Data;
using EVRenter_Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVRenter_Service
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task<Payment> CreateAsync(Payment payment)
        {
            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payment
                .Include(p => p.Booking)
                .Include(p => p.User)
                .ToListAsync();
        }

        
        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payment
                .Include(p => p.Booking)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        
        public async Task<List<Payment>> GetByBookingIdAsync(int bookingId)
        {
            return await _context.Payment
                .Include(p => p.User)
                .Where(p => p.BookingID == bookingId)
                .ToListAsync();
        }

        
        public async Task<bool> UpdateAsync(Payment payment)
        {
            var existing = await _context.Payment.FindAsync(payment.Id);
            if (existing == null)
                return false;

            _context.Entry(existing).CurrentValues.SetValues(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Payment.FindAsync(id);
            if (existing == null)
                return false;

            _context.Payment.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
