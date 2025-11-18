using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class AuditLog
    {
        [Key]
        public int LogId { get; set; }

        public int? UserId { get; set; }

        public string ActionType { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public string IPAddress { get; set; }
    }
}
