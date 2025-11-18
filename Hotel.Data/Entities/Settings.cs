using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Settings
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
