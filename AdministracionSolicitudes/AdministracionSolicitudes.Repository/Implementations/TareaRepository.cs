using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;
using MySqlConnector;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class TareaRepository : ITareaRepository
    {
        private const int ErrorFilaReferenciada = 1451;
        private readonly IDbConnectionFactory _connectionFactory;

        public TareaRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Tarea>> ObtenerSolicitudPaginacionAsync(int idSolicitud, int pagina, int tamanoPagina)
        {
            const string sql = @"
                SELECT t.id_tarea AS IdTarea,
                       t.id_solicitud AS IdSolicitud,
                       t.id_usuario AS IdUsuario,
                       u.nombre_completo AS NombreUsuario,
                       t.descripcion AS Descripcion,
                       t.fecha AS Fecha,
                       t.horas_invertidas AS HorasInvertidas
                FROM Tarea t
                INNER JOIN usuarios u ON u.id_usuario = t.id_usuario
                WHERE t.id_solicitud = @IdSolicitud
                ORDER BY t.fecha DESC, t.id_tarea DESC
                LIMIT @TamanoPagina OFFSET @Desplazamiento;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Tarea>(sql, new
            {
                IdSolicitud = idSolicitud,
                TamanoPagina = tamanoPagina,
                Desplazamiento = (pagina - 1) * tamanoPagina
            });
        }

        public async Task<int> ContarSolicitudAsync(int idSolicitud)
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM Tarea
                WHERE id_solicitud = @IdSolicitud;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new { IdSolicitud = idSolicitud });
        }

        public async Task<Tarea?> ObtenerIdAsync(int idTarea)
        {
            const string sql = @"
                SELECT t.id_tarea AS IdTarea,
                       t.id_solicitud AS IdSolicitud,
                       t.id_usuario AS IdUsuario,
                       u.nombre_completo AS NombreUsuario,
                       t.descripcion AS Descripcion,
                       t.fecha AS Fecha,
                       t.horas_invertidas AS HorasInvertidas
                FROM Tarea t
                INNER JOIN usuarios u ON u.id_usuario = t.id_usuario
                WHERE t.id_tarea = @IdTarea
                LIMIT 1;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Tarea>(sql, new { IdTarea = idTarea });
        }

        public async Task<int> InsertarAsync(Tarea tarea)
        {
            const string sql = @"
                INSERT INTO Tarea (id_solicitud, id_usuario, descripcion, fecha, horas_invertidas)
                VALUES (@IdSolicitud, @IdUsuario, @Descripcion, @Fecha, @HorasInvertidas);

                SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, tarea);
        }

        public async Task<bool> ActualizarAsync(Tarea tarea)
        {
            const string sql = @"
                UPDATE Tarea
                SET id_usuario = @IdUsuario,
                    descripcion = @Descripcion,
                    fecha = @Fecha,
                    horas_invertidas = @HorasInvertidas
                WHERE id_tarea = @IdTarea;";

            using var connection = _connectionFactory.CreateConnection();
            int filas = await connection.ExecuteAsync(sql, tarea);
            return filas > 0;
        }

        public async Task<ResultadoEliminacion> EliminarAsync(int idTarea)
        {
            const string sql = @"
                DELETE FROM Tarea
                WHERE id_tarea = @IdTarea;";

            using var connection = _connectionFactory.CreateConnection();

            try
            {
                int filas = await connection.ExecuteAsync(sql, new { IdTarea = idTarea });
                return filas > 0 ? ResultadoEliminacion.Eliminado : ResultadoEliminacion.NoEncontrado;
            }
            catch (MySqlException ex) when (ex.Number == ErrorFilaReferenciada)
            {
                return ResultadoEliminacion.TieneRelacionados;
            }
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            const string sql = @"
                SELECT id_usuario AS IdUsuario,
                       nombre_usuario  AS NombreUsuario,
                       nombre_completo AS NombreCompleto
                FROM usuarios
                ORDER BY nombre_completo;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Usuario>(sql);
        }

        public async Task<SolicitudResumen?> ObtenerSolicitudAsync(int idSolicitud)
        {
            const string sql = @"
                SELECT id_solicitud AS IdSolicitud,
                       consecutivo_oficio AS ConsecutivoOficio,
                       titulo AS Titulo
                FROM Solicitud
                WHERE id_solicitud = @IdSolicitud
                LIMIT 1;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<SolicitudResumen>(sql, new { IdSolicitud = idSolicitud });
        }

        public async Task<IEnumerable<SolicitudResumen>> ObtenerSolicitudesAsync()
        {
            const string sql = @"
                SELECT id_solicitud AS IdSolicitud,
                       consecutivo_oficio AS ConsecutivoOficio,
                       titulo AS Titulo
                FROM Solicitud
                ORDER BY consecutivo_oficio;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<SolicitudResumen>(sql);
        }
    }
}