
using AdministracionSolicitudes.API.Entities;

namespace AdministracionSolicitudes.API.Repository
{
    public interface IUserRepository
    {
        Task<Usuario?> GetByUsernameAsync(string username);
        Task UpdateFailedAttemptsAsync(int idUsuario, int intentosFallidos);
        Task ResetFailedAttemptsAsync(int idUsuario);
        Task BlockAsync(int idUsuario);
    }
}
