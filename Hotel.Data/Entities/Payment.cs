using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int? ReservationId { get; set; }
        public int? CheckInId { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public string PaymentMethod { get; set; }
        public string ReferenceNumber { get; set; }
    }
}
