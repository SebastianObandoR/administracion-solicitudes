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
        Task<Usuario?> GetByIdAsync(int idUsuario);
        Task<IEnumerable<Usuario>> GetAllAsync(int pagina, int cantidad);
        Task<int> CountAsync();
        Task<int> CreateAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int idUsuario);
        Task UpdateStatusAsync(int idUsuario, EstadoUsuario estado);
        Task UpdateFailedAttemptsAsync(int idUsuario, int intentosFallidos);
        Task ResetFailedAttemptsAsync(int idUsuario);
        Task BlockAsync(int idUsuario);

        Task<bool> HasRelatedDataAsync(int idUsuario);
    }
}
