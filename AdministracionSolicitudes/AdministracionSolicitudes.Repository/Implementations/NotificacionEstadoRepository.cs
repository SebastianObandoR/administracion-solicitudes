using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class NotificacionEstadoRepository : INotificacionEstadoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public NotificacionEstadoRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<DatosNotificacionEstado?> GetDatosAsync(int idSolicitud, int idEstadoAnterior, int idEstadoNuevo)
        {
            const string sql = @"
                SELECT s.id_solicitud AS IdSolicitud,
                       s.consecutivo_oficio AS ConsecutivoOficio,
                       s.titulo AS Titulo,
                       r.nombre AS NombreRepresentante,
                       r.correo AS CorreoRepresentante,
                       anterior.nombre AS EstadoAnterior,
                       nuevo.nombre AS EstadoNuevo
                FROM solicitud s
                INNER JOIN representante r ON r.id_representante = s.id_representante
                INNER JOIN estadosolicitud anterior ON anterior.id_estado = @IdEstadoAnterior
                INNER JOIN estadosolicitud nuevo ON nuevo.id_estado = @IdEstadoNuevo
                WHERE s.id_solicitud = @IdSolicitud;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<DatosNotificacionEstado>(sql, new
            {
                IdSolicitud = idSolicitud,
                IdEstadoAnterior = idEstadoAnterior,
                IdEstadoNuevo = idEstadoNuevo
            });
        }
    }
}
