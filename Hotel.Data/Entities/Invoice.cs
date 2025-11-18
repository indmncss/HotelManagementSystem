using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public int? ReservationId { get; set; }
        public int? CheckInId { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal Tax { get; set; }
        public decimal Discounts { get; set; }

        public string Status { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}
