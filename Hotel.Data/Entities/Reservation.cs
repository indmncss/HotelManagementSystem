using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Data.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("Guest")]
        public int GuestId { get; set; }

        public int? RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public int NumGuests { get; set; }
        public string Status { get; set; }

        public int? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Source { get; set; }
        public string Notes { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
    }
}
