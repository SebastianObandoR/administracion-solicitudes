using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;
using MySqlConnector;
using System.Text.Json;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class RepresentanteRepository : IRepresentanteRepository
    {
        private const string Modulo = "Representantes";
        private readonly IDbConnectionFactory _connectionFactory;

        public RepresentanteRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IReadOnlyList<Representante> Representantes, int Total)> ObtenerPaginaAsync(int pagina, int tamanoPagina, int usuarioId)
        {
            const string totalSql = "SELECT COUNT(*) FROM representante;";
            const string paginaSql = @"SELECT id_representante AS IdRepresentante, nombre AS Nombre, correo AS Correo
                                       FROM representante
                                       ORDER BY nombre, id_representante
                                       LIMIT @TamanoPagina OFFSET @Offset;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var total = await connection.QuerySingleAsync<int>(totalSql, transaction: transaction);
            var representantes = (await connection.QueryAsync<Representante>(
                paginaSql,
                new { TamanoPagina = tamanoPagina, Offset = (long)(pagina - 1) * tamanoPagina },
                transaction)).AsList();

            await RegistrarBitacoraAsync(connection, transaction, usuarioId, "CONSULT", "El usuario consulta representantes");
            transaction.Commit();

            return (representantes, total);
        }

        public async Task<Representante?> ObtenerPorIdAsync(int id, int usuarioId)
        {
            const string sql = @"SELECT id_representante AS IdRepresentante, nombre AS Nombre, correo AS Correo
                                 FROM representante
                                 WHERE id_representante = @Id;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var representante = await connection.QuerySingleOrDefaultAsync<Representante>(sql, new { Id = id }, transaction);
            await RegistrarBitacoraAsync(connection, transaction, usuarioId, "CONSULT", "El usuario consulta representantes");
            transaction.Commit();

            return representante;
        }

        public async Task<bool> CrearAsync(Representante representante, int usuarioId)
        {
            const string sql = @"INSERT INTO representante (nombre, correo)
                                 VALUES (@Nombre, @Correo);";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                await connection.ExecuteAsync(sql, representante, transaction);
                representante.IdRepresentante = await connection.QuerySingleAsync<int>("SELECT LAST_INSERT_ID();", transaction: transaction);

                await RegistrarBitacoraAsync(
                    connection,
                    transaction,
                    usuarioId,
                    "CREATE",
                    $"Se crea representante: {JsonSerializer.Serialize(representante)}");
                transaction.Commit();

                return true;
            }
            catch (MySqlException exception) when (exception.Number == 1062)
            {
                transaction.Rollback();
                return false;
            }
        }

        public async Task<ActualizarRepresentanteResultado> ActualizarAsync(Representante representante, int usuarioId)
        {
            const string obtenerSql = @"SELECT id_representante AS IdRepresentante, nombre AS Nombre, correo AS Correo
                                        FROM representante
                                        WHERE id_representante = @Id
                                        FOR UPDATE;";
            const string actualizarSql = @"UPDATE representante
                                           SET nombre = @Nombre, correo = @Correo
                                           WHERE id_representante = @IdRepresentante;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var anterior = await connection.QuerySingleOrDefaultAsync<Representante>(
                    obtenerSql,
                    new { Id = representante.IdRepresentante },
                    transaction);

                if (anterior is null)
                {
                    transaction.Rollback();
                    return ActualizarRepresentanteResultado.NoEncontrado;
                }

                await connection.ExecuteAsync(actualizarSql, representante, transaction);

                await RegistrarBitacoraAsync(
                    connection,
                    transaction,
                    usuarioId,
                    "UPDATE",
                    $"Se actualiza representante: {JsonSerializer.Serialize(new { anterior, actual = representante })}");
                transaction.Commit();

                return ActualizarRepresentanteResultado.Actualizado;
            }
            catch (MySqlException exception) when (exception.Number == 1062)
            {
                transaction.Rollback();
                return ActualizarRepresentanteResultado.CorreoDuplicado;
            }
        }

        public async Task<EliminarRepresentanteResultado> EliminarAsync(int id, int usuarioId)
        {
            const string obtenerSql = @"SELECT id_representante AS IdRepresentante, nombre AS Nombre, correo AS Correo
                                        FROM representante
                                        WHERE id_representante = @Id
                                        FOR UPDATE;";
            const string asignadoSql = "SELECT EXISTS(SELECT 1 FROM solicitud WHERE id_representante = @Id);";
            const string eliminarSql = "DELETE FROM representante WHERE id_representante = @Id;";

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var representante = await connection.QuerySingleOrDefaultAsync<Representante>(obtenerSql, new { Id = id }, transaction);

            if (representante is null)
            {
                transaction.Rollback();
                return EliminarRepresentanteResultado.NoEncontrado;
            }

            var asignado = await connection.QuerySingleAsync<bool>(asignadoSql, new { Id = id }, transaction);
            if (asignado)
            {
                transaction.Rollback();
                return EliminarRepresentanteResultado.TieneSolicitudes;
            }

            await connection.ExecuteAsync(eliminarSql, new { Id = id }, transaction);
            await RegistrarBitacoraAsync(
                connection,
                transaction,
                usuarioId,
                "DELETE",
                $"Se elimina representante: {JsonSerializer.Serialize(representante)}");
            transaction.Commit();

            return EliminarRepresentanteResultado.Eliminado;
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
