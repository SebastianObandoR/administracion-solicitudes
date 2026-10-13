using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IReporteFacturacionRepository
    {
        Task<IEnumerable<ReporteFacturacionMensual>> GetMensualAsync(int mes, int anio);
    }
}
