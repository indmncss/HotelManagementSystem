using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
