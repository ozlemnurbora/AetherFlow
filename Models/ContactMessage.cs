using System;
using System.ComponentModel.DataAnnotations;

namespace AetherFlow.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(120)]
        public string Email { get; set; }

        [Required]
        [StringLength(2000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
