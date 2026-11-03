using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AetherFlow.Models
{
    public class CartItem
    {
        public int DestinationId { get; set; }
        public string TourName { get; set; }
        public string CityName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
