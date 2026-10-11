using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Implementations
{
    internal static class SolicitudBitacoraPayload
    {
        public static object Crear(Solicitud s) => new
        {
            s.IdSolicitud,
            s.ConsecutivoOficio,
            s.Titulo,
            s.Descripcion,
            s.Observaciones,
            s.DocumentoInicio,
            s.DocumentoRespuesta,
            s.IdRepresentante,
            s.IdEstado,
            s.FechaIngreso,
            s.FechaRespuesta,
            s.FechaInicio
        };
    }
}
