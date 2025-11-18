using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class RateAdjustment
    {
        [Key]
        public int RateAdjustmentId { get; set; }

        public int RateId { get; set; }
        public decimal Adjustment { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
