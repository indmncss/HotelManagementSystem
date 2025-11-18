using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IDNumber { get; set; }
        public string Nationality { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
