using AdministracionSolicitudes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task UpdateFailedAttemptsAsync(int userId, int failedAttempts);
        Task ResetFailedAttemptsAsync(int userId);
        Task BlockAsync(int userId);
    }
}
