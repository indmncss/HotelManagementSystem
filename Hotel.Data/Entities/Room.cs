using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Data.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string RoomNumber { get; set; }

        public int? Floor { get; set; }

        [ForeignKey("RoomType")]
        public int RoomTypeId { get; set; }

        public string Status { get; set; }

        [ForeignKey("Rate")]
        public int? RateId { get; set; }

        public string Description { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual RoomType RoomType { get; set; }
        public virtual Rate Rate { get; set; }
    }
}
