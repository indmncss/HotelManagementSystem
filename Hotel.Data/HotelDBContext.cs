using System.Data.Entity;
using Hotel.Data.Entities;

namespace Hotel.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(string connString)
            : base(connString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<RoomStatusesHistory> RoomStatusesHistory { get; set; }
        public DbSet<HousekeepingTask> HousekeepingTasks { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<RateAdjustment> RateAdjustments { get; set; }
    }
}
