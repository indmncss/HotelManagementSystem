using System;
using Hotel.Data.Entities;
using Hotel.Data.Repositories;

namespace Hotel.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Role> Roles { get; }
        IRepository<Guest> Guests { get; }
        IRepository<RoomType> RoomTypes { get; }
        IRepository<Rate> Rates { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<CheckIn> CheckIns { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<Payment> Payments { get; }
        IRepository<AuditLog> AuditLogs { get; }
        IRepository<HousekeepingTask> HousekeepingTasks { get; }
        IRepository<RoomStatusesHistory> RoomStatusesHistory { get; }
        IRepository<Settings> Settings { get; }
        IRepository<Extra> Extras { get; }
        IRepository<RateAdjustment> RateAdjustments { get; }

        int Complete();
    }
}
