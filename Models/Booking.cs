using System;
using System.Collections.Generic;

namespace AetherFlow.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string BookingCode { get; set; }   
        public string UserId { get; set; }        
        public DateTime CreatedAt { get; set; }

        public decimal Subtotal { get; set; }
        public decimal ServiceFee { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<BookingItem> Items { get; set; }
    }
}
