using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IReporteFacturacionService
    {
        Task<IEnumerable<ReporteFacturacionMensual>> GetMensualAsync(int mes, int anio, int idUsuario);
    }
}
