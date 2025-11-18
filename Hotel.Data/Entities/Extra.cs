using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Extra
    {
        [Key]
        public int ExtraId { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
