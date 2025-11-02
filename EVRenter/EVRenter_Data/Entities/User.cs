using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVRenter_Data.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public int? RoleID { get; set; }
        public bool IsActive { get; set; } = false;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<HandoverAndReturn> HandoverAndReturns { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<ExtraFee> ExtraFees { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual StaffProfile StaffProfile { get; set; }
        public virtual RenterProfile RenterProfile { get; set; }
    }
}
