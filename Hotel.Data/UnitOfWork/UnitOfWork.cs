using System;
using Hotel.Data.Entities;
using Hotel.Data.Repositories;

namespace Hotel.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _context;

        public IRepository<User> Users { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Guest> Guests { get; }
        public IRepository<RoomType> RoomTypes { get; }
        public IRepository<Rate> Rates { get; }
        public IRepository<Room> Rooms { get; }
        public IRepository<Reservation> Reservations { get; }
        public IRepository<CheckIn> CheckIns { get; }
        public IRepository<Invoice> Invoices { get; }
        public IRepository<Payment> Payments { get; }
        public IRepository<AuditLog> AuditLogs { get; }
        public IRepository<HousekeepingTask> HousekeepingTasks { get; }
        public IRepository<RoomStatusesHistory> RoomStatusesHistory { get; }
        public IRepository<Settings> Settings { get; }
        public IRepository<Extra> Extras { get; }
        public IRepository<RateAdjustment> RateAdjustments { get; }

        public UnitOfWork(HotelDbContext context)
        {
            _context = context;

            Users = new EfRepository<User>(context);
            Roles = new EfRepository<Role>(context);
            Guests = new EfRepository<Guest>(context);
            RoomTypes = new EfRepository<RoomType>(context);
            Rates = new EfRepository<Rate>(context);
            Rooms = new EfRepository<Room>(context);
            Reservations = new EfRepository<Reservation>(context);
            CheckIns = new EfRepository<CheckIn>(context);
            Invoices = new EfRepository<Invoice>(context);
            Payments = new EfRepository<Payment>(context);
            AuditLogs = new EfRepository<AuditLog>(context);
            HousekeepingTasks = new EfRepository<HousekeepingTask>(context);
            RoomStatusesHistory = new EfRepository<RoomStatusesHistory>(context);
            Settings = new EfRepository<Settings>(context);
            Extras = new EfRepository<Extra>(context);
            RateAdjustments = new EfRepository<RateAdjustment>(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
