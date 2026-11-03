using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AetherFlow.Models
{
    public class DbCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<DbCartItem> Items { get; set; } = new List<DbCartItem>();
    }

    public class DbCartItem
    {
        public int Id { get; set; }

        public int DbCartId { get; set; }
        public virtual DbCart DbCart { get; set; }

        public int DestinationId { get; set; }

        [Required]
        public string TourName { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
