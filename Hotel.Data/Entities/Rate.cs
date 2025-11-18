using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Rate
    {
        [Key]
        public int RateId { get; set; }

        [Required]
        public string RateName { get; set; }

        public decimal PricePerNight { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }
    }
}
