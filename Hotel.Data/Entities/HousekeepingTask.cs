using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class HousekeepingTask
    {
        [Key]
        public int TaskId { get; set; }

        public int RoomId { get; set; }
        public int? AssignedToUserId { get; set; }

        public string Status { get; set; }
        public string Notes { get; set; }

        public DateTime? ScheduledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
