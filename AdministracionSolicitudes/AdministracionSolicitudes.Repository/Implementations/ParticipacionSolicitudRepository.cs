using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Repository.Models;
using Dapper;
using MySqlConnector;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class ParticipacionSolicitudRepository : IParticipacionSolicitudRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ParticipacionSolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IReadOnlyList<ParticipacionSolicitud> Participaciones, bool SolicitudTieneAprobada)> GetPorSolicitudAsync(int idSolicitud)
        {
            const string sql = @"
                SELECT p.id_participacion AS IdParticipacion,
                       p.id_solicitud AS IdSolicitud,
                       p.id_proveedor AS IdProveedor,
                       pr.cedula_juridica AS CedulaJuridica,
                       pr.nombre AS NombreProveedor,
                       p.fecha_participacion AS FechaParticipacion,
                       p.aprobada AS Aprobada
                FROM participacion p
                INNER JOIN proveedor pr ON pr.id_proveedor = p.id_proveedor
                WHERE p.id_solicitud = @IdSolicitud
                ORDER BY p.aprobada DESC, pr.nombre, p.fecha_participacion;";
            const string aprobadaSql = @"
                SELECT EXISTS(
                    SELECT 1 FROM participacion
                    WHERE id_solicitud = @IdSolicitud AND aprobada = 1
                );";

            using var connection = _connectionFactory.CreateConnection();
            var rows = (await connection.QueryAsync<ParticipacionSolicitud>(sql, new { IdSolicitud = idSolicitud })).AsList();
            bool solicitudTieneAprobada = await connection.ExecuteScalarAsync<bool>(aprobadaSql, new { IdSolicitud = idSolicitud });
            return (rows, solicitudTieneAprobada);
        }

        public async Task<ParticipacionDetalle?> GetDetalleAsync(int idSolicitud, int idParticipacion)
        {
            const string participacionSql = @"
                SELECT p.id_participacion AS IdParticipacion,
                       p.id_solicitud AS IdSolicitud,
                       p.id_proveedor AS IdProveedor,
                       pr.cedula_juridica AS CedulaJuridica,
                       pr.nombre AS NombreProveedor,
                       p.fecha_participacion AS FechaParticipacion,
                       p.aprobada AS Aprobada
                FROM participacion p
                INNER JOIN proveedor pr ON pr.id_proveedor = p.id_proveedor
                WHERE p.id_solicitud = @IdSolicitud
                  AND p.id_participacion = @IdParticipacion;";
            const string desglosesSql = @"
                SELECT id_participacion AS IdParticipacion, mes AS Mes, anio AS Anio,
                       horas AS Horas, monto AS Monto, iva AS Iva, total AS Total,
                       observaciones AS Observaciones, porcentaje_cobro AS PorcentajeCobro
                FROM participaciondesglose
                WHERE id_participacion = @IdParticipacion
                ORDER BY anio, mes;";
            const string aprobadaSql = @"
                SELECT EXISTS(
                    SELECT 1 FROM participacion
                    WHERE id_solicitud = @IdSolicitud AND aprobada = 1
                );";

            using var connection = _connectionFactory.CreateConnection();
            var parametros = new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion };
            var participacion = await connection.QuerySingleOrDefaultAsync<ParticipacionSolicitud>(participacionSql, parametros);
            if (participacion == null) return null;

            var desgloses = (await connection.QueryAsync<DesglosePropuesta>(desglosesSql, parametros)).AsList();
            bool solicitudTieneAprobada = await connection.ExecuteScalarAsync<bool>(aprobadaSql, new { IdSolicitud = idSolicitud });
            return new ParticipacionDetalle
            {
                Participacion = participacion,
                Desgloses = desgloses,
                SolicitudTieneAprobada = solicitudTieneAprobada
            };
        }

        public async Task<EstadoAprobacionParticipacion> AprobarAsync(int idSolicitud, int idParticipacion)
        {
            using var connection = _connectionFactory.CreateConnection();
            if (connection is not MySqlConnection mysqlConnection)
                throw new InvalidOperationException("La conexión de base de datos debe ser MySQL.");

            await mysqlConnection.OpenAsync();
            await using var transaction = await mysqlConnection.BeginTransactionAsync();

            // Bloquear la solicitud serializa aprobaciones concurrentes de la misma solicitud.
            int? solicitudEncontrada = await mysqlConnection.QuerySingleOrDefaultAsync<int?>(@"
                SELECT id_solicitud FROM solicitud WHERE id_solicitud = @IdSolicitud FOR UPDATE;",
                new { IdSolicitud = idSolicitud }, transaction);
            if (solicitudEncontrada == null) return EstadoAprobacionParticipacion.SolicitudNoEncontrada;

            var participacion = await mysqlConnection.QuerySingleOrDefaultAsync<ParticipacionSolicitud>(@"
                SELECT id_participacion AS IdParticipacion,
                       id_solicitud AS IdSolicitud,
                       id_proveedor AS IdProveedor,
                       aprobada AS Aprobada
                FROM participacion
                WHERE id_solicitud = @IdSolicitud AND id_participacion = @IdParticipacion
                FOR UPDATE;", new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion }, transaction);
            if (participacion == null) return EstadoAprobacionParticipacion.ParticipacionNoEncontrada;

            bool yaHayAprobada = await mysqlConnection.ExecuteScalarAsync<bool>(@"
                SELECT EXISTS(SELECT 1 FROM participacion
                    WHERE id_solicitud = @IdSolicitud AND aprobada = 1);",
                new { IdSolicitud = idSolicitud }, transaction);
            if (yaHayAprobada) return EstadoAprobacionParticipacion.SolicitudYaTieneAprobada;

            int cantidadDesgloses = await mysqlConnection.ExecuteScalarAsync<int>(@"
                SELECT COUNT(*) FROM participaciondesglose WHERE id_participacion = @IdParticipacion;",
                new { IdParticipacion = idParticipacion }, transaction);
            if (cantidadDesgloses == 0) return EstadoAprobacionParticipacion.PropuestaSinDesgloses;

            await mysqlConnection.ExecuteAsync(
                "DELETE FROM desglosesolicitud WHERE id_solicitud = @IdSolicitud;",
                new { IdSolicitud = idSolicitud }, transaction);

            int insertados = await mysqlConnection.ExecuteAsync(@"
                INSERT INTO desglosesolicitud
                    (id_solicitud, mes, anio, horas, monto, iva, total, observaciones, porcentaje_cobro)
                SELECT @IdSolicitud, mes, anio, horas, monto,
                       ROUND(monto * 0.13, 2), monto + ROUND(monto * 0.13, 2),
                       observaciones, porcentaje_cobro
                FROM participaciondesglose
                WHERE id_participacion = @IdParticipacion;",
                new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion }, transaction);
            if (insertados != cantidadDesgloses)
                throw new InvalidOperationException("No se pudieron asociar todos los desgloses de la propuesta.");

            await mysqlConnection.ExecuteAsync(@"
                UPDATE participacion
                SET aprobada = 1
                WHERE id_solicitud = @IdSolicitud AND id_participacion = @IdParticipacion;",
                new { IdSolicitud = idSolicitud, IdParticipacion = idParticipacion }, transaction);

            await transaction.CommitAsync();
            return EstadoAprobacionParticipacion.Aprobada;
        }
    }
}
