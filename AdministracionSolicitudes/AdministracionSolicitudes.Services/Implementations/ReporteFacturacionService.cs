using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class ReporteFacturacionService : IReporteFacturacionService
    {
        private const string Modulo = "Reporte de facturacion mensual";
        private readonly IReporteFacturacionRepository _repository;
        private readonly IAuditoriaService _auditoria;

        public ReporteFacturacionService(IReporteFacturacionRepository repository, IAuditoriaService auditoria)
        {
            _repository = repository;
            _auditoria = auditoria;
        }

        public async Task<IEnumerable<ReporteFacturacionMensual>> GetMensualAsync(int mes, int anio, int idUsuario)
        {
            try
            {
                var registros = await _repository.GetMensualAsync(mes, anio);
                await _auditoria.RegistrarConsultaAsync(idUsuario, Modulo, "reporte de facturacion mensual");
                return registros;
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                throw;
            }
        }
    }
}
