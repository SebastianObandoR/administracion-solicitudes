using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private const string SelectBase = @"
            SELECT
                s.id_solicitud        AS IdSolicitud,
                s.consecutivo_oficio  AS ConsecutivoOficio,
                s.titulo              AS Titulo,
                s.descripcion         AS Descripcion,
                s.observaciones       AS Observaciones,
                s.documento_inicio    AS DocumentoInicio,
                s.documento_respuesta AS DocumentoRespuesta,
                s.id_representante    AS IdRepresentante,
                s.id_estado           AS IdEstado,
                s.fecha_ingreso       AS FechaIngreso,
                s.fecha_respuesta     AS FechaRespuesta,
                s.fecha_inicio        AS FechaInicio,
                r.nombre              AS NombreRepresentante,
                e.nombre              AS NombreEstado
            FROM Solicitud s
            INNER JOIN Representante r ON r.id_representante = s.id_representante
            INNER JOIN EstadoSolicitud e ON e.id_estado = s.id_estado";

        private readonly IDbConnectionFactory _connectionFactory;

        public SolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IEnumerable<Solicitud> Items, int TotalRegistros)> GetPagedAsync(int pageNumber, int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            const string countSql = "SELECT COUNT(*) FROM Solicitud;";
            string pagedSql = $"{SelectBase} ORDER BY s.id_solicitud DESC LIMIT @PageSize OFFSET @Offset;";

            using var connection = _connectionFactory.CreateConnection();

            int total = await connection.ExecuteScalarAsync<int>(countSql);
            var items = await connection.QueryAsync<Solicitud>(pagedSql, new { PageSize = pageSize, Offset = offset });

            return (items, total);
        }

        public async Task<Solicitud?> GetByIdAsync(int idSolicitud)
        {
            string sql = $"{SelectBase} WHERE s.id_solicitud = @IdSolicitud;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Solicitud>(sql, new { IdSolicitud = idSolicitud });
        }

        public async Task<bool> ExistsConsecutivoAsync(string consecutivoOficio, int? excludeId = null)
        {
            const string sql = @"SELECT COUNT(*) FROM Solicitud
                                  WHERE consecutivo_oficio = @ConsecutivoOficio
                                  AND (@ExcludeId IS NULL OR id_solicitud <> @ExcludeId);";
            using var connection = _connectionFactory.CreateConnection();
            int count = await connection.ExecuteScalarAsync<int>(sql, new { ConsecutivoOficio = consecutivoOficio, ExcludeId = excludeId });
            return count > 0;
        }

        public async Task<int> CreateAsync(Solicitud solicitud)
        {
            const string sql = @"
                INSERT INTO Solicitud
                    (consecutivo_oficio, titulo, descripcion, observaciones, documento_inicio,
                     documento_respuesta, id_representante, id_estado, fecha_ingreso, fecha_respuesta, fecha_inicio)
                VALUES
                    (@ConsecutivoOficio, @Titulo, @Descripcion, @Observaciones, @DocumentoInicio,
                     @DocumentoRespuesta, @IdRepresentante, @IdEstado, @FechaIngreso, @FechaRespuesta, @FechaInicio);
                SELECT LAST_INSERT_ID();";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, solicitud);
        }

        public async Task UpdateAsync(Solicitud solicitud)
        {
            const string sql = @"
                UPDATE Solicitud SET
                    titulo = @Titulo,
                    descripcion = @Descripcion,
                    observaciones = @Observaciones,
                    documento_inicio = @DocumentoInicio,
                    documento_respuesta = @DocumentoRespuesta,
                    id_representante = @IdRepresentante,
                    id_estado = @IdEstado,
                    fecha_ingreso = @FechaIngreso,
                    fecha_respuesta = @FechaRespuesta,
                    fecha_inicio = @FechaInicio
                WHERE id_solicitud = @IdSolicitud;";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, solicitud);
        }

        public async Task DeleteAsync(int idSolicitud)
        {
            const string sql = "DELETE FROM Solicitud WHERE id_solicitud = @IdSolicitud;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdSolicitud = idSolicitud });
        }

        public async Task<bool> TieneRegistrosRelacionadosAsync(int idSolicitud)
        {
            const string sql = @"
                SELECT
                    (SELECT COUNT(*) FROM DesgloseSolicitud WHERE id_solicitud = @IdSolicitud) +
                    (SELECT COUNT(*) FROM Tarea WHERE id_solicitud = @IdSolicitud) +
                    (SELECT COUNT(*) FROM HistorialEstadoSolicitud WHERE id_solicitud = @IdSolicitud);";

            using var connection = _connectionFactory.CreateConnection();
            int relacionados = await connection.ExecuteScalarAsync<int>(sql, new { IdSolicitud = idSolicitud });
            return relacionados > 0;
        }

        public async Task<IEnumerable<Solicitud>> GetVencibleCandidatesAsync()
        {
            string sql = $"{SelectBase} WHERE s.fecha_respuesta IS NULL;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Solicitud>(sql);
        }

        public async Task MarcarComoVencidaAsync(int idSolicitud, int idEstadoVencida)
        {
            const string sql = "UPDATE Solicitud SET id_estado = @IdEstadoVencida WHERE id_solicitud = @IdSolicitud;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdSolicitud = idSolicitud, IdEstadoVencida = idEstadoVencida });
        }
    }
}
