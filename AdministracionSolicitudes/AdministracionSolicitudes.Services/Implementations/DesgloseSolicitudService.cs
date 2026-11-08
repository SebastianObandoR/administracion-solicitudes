using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using MySqlConnector;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class DesgloseSolicitudService : IDesgloseSolicitudService
    {
        private const string Modulo = "Desgloses de solicitudes";
        private const string MensajeDuplicado = "Ya existe un desglose para el mes y año seleccionados.";
        private const string MensajeRelacionado = "No se puede eliminar un registro con datos relacionados.";
        private readonly IDesgloseSolicitudRepository _repository;
        private readonly IAuditoriaService _auditoria;

        public DesgloseSolicitudService(IDesgloseSolicitudRepository repository, IAuditoriaService auditoria)
        {
            _repository = repository;
            _auditoria = auditoria;
        }

        public async Task<(IEnumerable<DesgloseSolicitud> Items, int TotalRegistros)> GetPagedAsync(
            int idSolicitud, int pageNumber, int pageSize, int idUsuario)
        {
            try
            {
                var resultado = await _repository.GetPagedAsync(idSolicitud, pageNumber, pageSize);
                await _auditoria.RegistrarConsultaAsync(idUsuario, Modulo, "desgloses de la solicitud");
                return resultado;
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                throw;
            }
        }

        public async Task<DesgloseSolicitud?> GetByIdAsync(int idSolicitud, int mes, int anio, int idUsuario)
        {
            try
            {
                var desglose = await _repository.GetByIdAsync(idSolicitud, mes, anio);
                if (desglose != null) await _auditoria.RegistrarConsultaAsync(idUsuario, Modulo, "desglose de la solicitud");
                return desglose;
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                throw;
            }
        }

        public Task<bool> SolicitudTieneParticipacionAprobadaAsync(int idSolicitud)
            => _repository.SolicitudTieneParticipacionAprobadaAsync(idSolicitud);

        public async Task<ResultadoOperacion> CreateAsync(DesgloseSolicitud desglose, int idUsuario)
        {
            try
            {
                if (await _repository.SolicitudTieneParticipacionAprobadaAsync(desglose.IdSolicitud))
                    return ResultadoOperacion.Error("La solicitud ya tiene una propuesta aprobada; sus desgloses no se pueden modificar.");
                if (await _repository.ExistsAsync(desglose.IdSolicitud, desglose.Mes, desglose.Anio))
                    return ResultadoOperacion.Error(MensajeDuplicado);

                CalcularTotales(desglose);
                await _repository.CreateAsync(desglose);
                await _auditoria.RegistrarCreacionAsync(idUsuario, Modulo, desglose);
                return ResultadoOperacion.Exito("Desglose creado correctamente.");
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                return ResultadoOperacion.Error("Ocurrio un error al crear el desglose.");
            }
        }

        public async Task<ResultadoOperacion> UpdateAsync(
            DesgloseSolicitud desglose, int originalMes, int originalAnio, int idUsuario)
        {
            try
            {
                if (await _repository.SolicitudTieneParticipacionAprobadaAsync(desglose.IdSolicitud))
                    return ResultadoOperacion.Error("La solicitud ya tiene una propuesta aprobada; sus desgloses no se pueden modificar.");
                var anterior = await _repository.GetByIdAsync(desglose.IdSolicitud, originalMes, originalAnio);
                if (anterior == null) return ResultadoOperacion.Error("El desglose no existe.");
                if (await _repository.ExistsAsync(desglose.IdSolicitud, desglose.Mes, desglose.Anio, originalMes, originalAnio))
                    return ResultadoOperacion.Error(MensajeDuplicado);

                CalcularTotales(desglose);
                if (!await _repository.UpdateAsync(desglose, originalMes, originalAnio))
                    return ResultadoOperacion.Error("El desglose no existe.");

                await _auditoria.RegistrarActualizacionAsync(idUsuario, Modulo, anterior, desglose);
                return ResultadoOperacion.Exito("Desglose actualizado correctamente.");
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                return ResultadoOperacion.Error("Ocurrio un error al actualizar el desglose.");
            }
        }

        public async Task<ResultadoOperacion> DeleteAsync(int idSolicitud, int mes, int anio, int idUsuario)
        {
            try
            {
                if (await _repository.SolicitudTieneParticipacionAprobadaAsync(idSolicitud))
                    return ResultadoOperacion.Error("La solicitud ya tiene una propuesta aprobada; sus desgloses no se pueden modificar.");
                var existente = await _repository.GetByIdAsync(idSolicitud, mes, anio);
                if (existente == null) return ResultadoOperacion.Error("El desglose no existe.");
                if (!await _repository.DeleteAsync(idSolicitud, mes, anio))
                    return ResultadoOperacion.Error("El desglose no existe.");

                await _auditoria.RegistrarEliminacionAsync(idUsuario, Modulo, existente);
                return ResultadoOperacion.Exito("Desglose eliminado correctamente.");
            }
            catch (MySqlException ex) when (ex.Number == 1451)
            {
                return ResultadoOperacion.Error(MensajeRelacionado);
            }
            catch (Exception ex)
            {
                await _auditoria.RegistrarErrorSeguroAsync(idUsuario, Modulo, ex);
                return ResultadoOperacion.Error("Ocurrio un error al eliminar el desglose.");
            }
        }

        private static void CalcularTotales(DesgloseSolicitud desglose)
        {
            desglose.Iva = Math.Round(desglose.Monto * 0.13m, 2, MidpointRounding.AwayFromZero);
            desglose.Total = desglose.Monto + desglose.Iva;
        }
    }
}
