using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IBitacoraRepository
    {
        Task RegistrarAsync(Bitacora bitacora);
    }
}
