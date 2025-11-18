using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Data.Entities
{
    public class CheckIn
    {
        [Key]
        public int CheckInId { get; set; }

        public int? ReservationId { get; set; }

        public DateTime ActualCheckInDateTime { get; set; }
        public DateTime ExpectedCheckOutDateTime { get; set; }
        public DateTime? ActualCheckOutDateTime { get; set; }

        public decimal DepositAmount { get; set; }
        public string PaymentMethod { get; set; }

        public int? HandledByUserId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
