using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Data.Entities
{
    public class Payment : BaseEntity
    {
        public int BookingID { get; set; }
        public int UserID { get; set; }
        public int PaymentType { get; set; }
        public int PaymentMethod {  get; set; }
        public DateTime PaymentTime { get; set; }
        [Precision(18, 2)] 
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual User User { get; set; }
    }
}
