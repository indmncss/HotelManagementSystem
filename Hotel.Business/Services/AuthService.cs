using System;
using System.Linq;
using Hotel.Common.Security;
using Hotel.Data.UnitOfWork;
using Hotel.Data.Entities;

namespace Hotel.Business.Services
{
    public class AuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly int _maxFailedAttempts;
        private readonly int _lockoutMinutes;

        public AuthService(IUnitOfWork uow, int maxFailedAttempts = 5, int lockoutMinutes = 15)
        {
            _uow = uow;
            _maxFailedAttempts = maxFailedAttempts;
            _lockoutMinutes = lockoutMinutes;
        }

        public User Authenticate(string username, string password, out string message)
        {
            message = null;

            var user = _uow.Users.Find(u => u.Username == username).FirstOrDefault();

            if (user == null)
            {
                message = "Invalid username or password.";
                return null;
            }

            if (!user.IsActive)
            {
                message = "Account inactive.";
                return null;
            }

            // Lockout check
            if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.UtcNow)
            {
                message = $"Account locked until {user.LockedUntil.Value.ToLocalTime()}";
                return null;
            }

            // Verify password
            bool verified = PasswordHelper.VerifyPassword(
                password,
                user.PasswordHash,
                user.PasswordSalt,
                user.PasswordIterations
            );

            if (!verified)
            {
                user.FailedLoginAttempts++;

                if (user.FailedLoginAttempts >= _maxFailedAttempts)
                {
                    user.LockedUntil = DateTime.UtcNow.AddMinutes(_lockoutMinutes);
                    message = $"Too many failed attempts. Account locked for {_lockoutMinutes} minutes.";
                }
                else
                {
                    message = "Invalid username or password.";
                }

                _uow.Users.Update(user);
                _uow.Complete();
                return null;
            }

            // SUCCESS
            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;
            user.LastLogin = DateTime.UtcNow;


            _uow.Users.Update(user);
            _uow.Complete();

            user.Role = _uow.Roles.GetById(user.RoleId);

            return user;
        }

        public bool ResetPassword(int userId, string newPassword)
        {
            var user = _uow.Users.GetById(userId);
            if (user == null) return false;

            string salt;
            user.PasswordHash = PasswordHelper.HashPassword(newPassword, out salt);
            user.PasswordSalt = salt;
            user.PasswordIterations = 100000;

            user.FailedLoginAttempts = 0;
            user.LockedUntil = null;

            _uow.Users.Update(user);
            _uow.Complete();

            return true;
        }
    }
}
