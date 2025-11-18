using System;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Entities
{
    public class RoomStatusesHistory
    {
        [Key]
        public int HistoryId { get; set; }

        public int RoomId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }

        public DateTime ChangedAt { get; set; }
        public int? ChangedByUserId { get; set; }
    }
}
