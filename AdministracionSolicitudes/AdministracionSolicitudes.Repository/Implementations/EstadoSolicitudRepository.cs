using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;
using MySqlConnector;
using System.Text.Json;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class EstadoSolicitudRepository : IEstadoSolicitudRepository
    {
        private const string Modulo = "Estados de solicitudes";
        private readonly IDbConnectionFactory _connectionFactory;

        public EstadoSolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IReadOnlyList<EstadoSolicitud> Estados, int Total)> ObtenerPaginaAsync(
            int pagina,
            int tamanoPagina,
            int usuarioId)
        {
            const string totalSql = "SELECT COUNT(*) FROM estadosolicitud;";
            const string paginaSql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                       FROM estadosolicitud
                                       ORDER BY id_estado
                                       LIMIT @TamanoPagina OFFSET @Offset;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var total = await connection.QuerySingleAsync<int>(totalSql, transaction: transaction);
            var estados = (await connection.QueryAsync<EstadoSolicitud>(
                paginaSql,
                new { TamanoPagina = tamanoPagina, Offset = (long)(pagina - 1) * tamanoPagina },
                transaction)).AsList();

            await RegistrarBitacoraAsync(connection, transaction, usuarioId, "CONSULT", "El usuario consulta estados");
            transaction.Commit();

            return (estados, total);
        }

        public async Task<EstadoSolicitud?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            const string sql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                 FROM estadosolicitud
                                 WHERE id_estado = @Id;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var estado = await connection.QuerySingleOrDefaultAsync<EstadoSolicitud>(
                sql,
                new { Id = id },
                transaction);

            await RegistrarBitacoraAsync(connection, transaction, usuarioId, "CONSULT", "El usuario consulta estados");
            transaction.Commit();

            return estado;
        }

        public async Task<bool> CrearAsync(EstadoSolicitud estado, int usuarioId)
        {
            const string sql = "INSERT INTO estadosolicitud (nombre) VALUES (@Nombre);";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(sql, estado, transaction);
                estado.IdEstado = await connection.QuerySingleAsync<int>(
                    "SELECT LAST_INSERT_ID();",
                    transaction: transaction);

                await RegistrarBitacoraAsync(
                    connection,
                    transaction,
                    usuarioId,
                    "CREATE",
                    $"Se crea estado: {JsonSerializer.Serialize(estado)}");
                transaction.Commit();

                return true;
            }
            catch (MySqlException exception) when (exception.Number == 1062)
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<ActualizarEstadoSolicitudResultado> ActualizarAsync(EstadoSolicitud estado, int usuarioId)
        {
            const string obtenerSql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                        FROM estadosolicitud
                                        WHERE id_estado = @Id
                                        FOR UPDATE;";
            const string actualizarSql = @"UPDATE estadosolicitud
                                           SET nombre = @Nombre
                                           WHERE id_estado = @IdEstado;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var anterior = await connection.QuerySingleOrDefaultAsync<EstadoSolicitud>(
                    obtenerSql,
                    new { Id = estado.IdEstado },
                    transaction);

                if (anterior is null)
                {
                    transaction.Rollback();
                    return ActualizarEstadoSolicitudResultado.NoEncontrado;
                }

                await connection.ExecuteAsync(actualizarSql, estado, transaction);
                await RegistrarBitacoraAsync(
                    connection,
                    transaction,
                    usuarioId,
                    "UPDATE",
                    $"Se actualiza estado: {JsonSerializer.Serialize(new { anterior, actual = estado })}");
                transaction.Commit();

                return ActualizarEstadoSolicitudResultado.Actualizado;
            }
            catch (MySqlException exception) when (exception.Number == 1062)
            {
                transaction.Rollback();
                return ActualizarEstadoSolicitudResultado.NombreDuplicado;
            }
        }

        public async Task<EliminarEstadoSolicitudResultado> EliminarAsync(int id, int usuarioId)
        {
            const string obtenerSql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                        FROM estadosolicitud
                                        WHERE id_estado = @Id
                                        FOR UPDATE;";
            const string asignadoSql = @"SELECT EXISTS(
                                             SELECT 1 FROM solicitud WHERE id_estado = @Id
                                             UNION ALL
                                             SELECT 1 FROM historialestadosolicitud
                                             WHERE id_estado_anterior = @Id OR id_estado_nuevo = @Id
                                         );";
            const string eliminarSql = "DELETE FROM estadosolicitud WHERE id_estado = @Id;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var estado = await connection.QuerySingleOrDefaultAsync<EstadoSolicitud>(
                obtenerSql,
                new { Id = id },
                transaction);

            if (estado is null)
            {
                transaction.Rollback();
                return EliminarEstadoSolicitudResultado.NoEncontrado;
            }

            var asignado = await connection.QuerySingleAsync<bool>(
                asignadoSql,
                new { Id = id },
                transaction);

            if (asignado)
            {
                transaction.Rollback();
                return EliminarEstadoSolicitudResultado.TieneSolicitudes;
            }

            await connection.ExecuteAsync(eliminarSql, new { Id = id }, transaction);
            await RegistrarBitacoraAsync(
                connection,
                transaction,
                usuarioId,
                "DELETE",
                $"Se elimina estado: {JsonSerializer.Serialize(estado)}");
            transaction.Commit();

            return EliminarEstadoSolicitudResultado.Eliminado;
        }

        public async Task RegistrarErrorAsync(int usuarioId, string mensaje)
        {
            const string sql = @"INSERT INTO errorlog (id_usuario, modulo, mensaje_error, fecha)
                                 VALUES (@UsuarioId, @Modulo, @Mensaje, CURRENT_TIMESTAMP);";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { UsuarioId = usuarioId, Modulo, Mensaje = mensaje });
        }

        private static Task RegistrarBitacoraAsync(
            System.Data.IDbConnection connection,
            System.Data.IDbTransaction transaction,
            int usuarioId,
            string accion,
            string descripcion)
        {
            const string sql = @"INSERT INTO bitacora (id_usuario, modulo, accion, descripcion, fecha)
                                 VALUES (@UsuarioId, @Modulo, @Accion, @Descripcion, CURRENT_TIMESTAMP);";

            return connection.ExecuteAsync(
                sql,
                new { UsuarioId = usuarioId, Modulo, Accion = accion, Descripcion = descripcion },
                transaction);
        }
    }
}
