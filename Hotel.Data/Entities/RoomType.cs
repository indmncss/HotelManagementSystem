using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class RoomType
    {
        [Key]
        public int RoomTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        public int MaxOccupancy { get; set; }

        public string Description { get; set; }
    }
}
