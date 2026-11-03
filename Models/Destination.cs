using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AetherFlow.Models
{
    public class Destination
    {
        public int Id { get; set; }

        // 1. BASIC INFORMATION
        [Required]
        [Display(Name = "Tour Name")]
        public string TourName { get; set; } // Example: Mystical Cappadocia

        [Required]
        [Display(Name = "City / Country")]
        public string CityName { get; set; } // Example: Nevşehir, Turkey

        [Required]
        [Display(Name = "Description & Itinerary")]
        [DataType(DataType.MultilineText)] // Large text area
        public string Description { get; set; }

        // 2. SERVICE DETAILS
        [Required]
        [Display(Name = "What's Included?")]
        [DataType(DataType.MultilineText)] // Large text area
        public string WhatsIncluded { get; set; } // Example: Hotel, Breakfast, Guide

        [Required]
        [Display(Name = "Meeting Point")]
        public string MeetingPoint { get; set; } // Example: Istanbul Airport Gate 1

        // 3. DATE INFORMATION
        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)] // Calendar picker
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)] // Calendar picker
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; } // Example: 3 Days / 2 Nights

        // 4. TRANSPORTATION
        [Required]
        [Display(Name = "Transportation")]
        public string Transportation { get; set; } // Plane, Bus, VIP Van

        [Required]
        [Display(Name = "Price ($)")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        [DisplayName("Tour Capacity")]
        [Range(0, 10000, ErrorMessage = "Capacity cannot be less than 0!")] // Prevents negative values
        public int Capacity { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Is Popular?")]
        public bool IsPopular { get; set; }
    }
}
