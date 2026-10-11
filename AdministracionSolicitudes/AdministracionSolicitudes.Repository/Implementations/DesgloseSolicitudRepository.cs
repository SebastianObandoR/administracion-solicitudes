using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class DesgloseSolicitudRepository : IDesgloseSolicitudRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DesgloseSolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<(IEnumerable<DesgloseSolicitud> Items, int TotalRegistros)> GetPagedAsync(
            int idSolicitud, int pageNumber, int pageSize)
        {
            const string countSql = "SELECT COUNT(*) FROM desglosesolicitud WHERE id_solicitud = @IdSolicitud;";
            const string itemsSql = @"
                SELECT id_solicitud AS IdSolicitud, mes AS Mes, anio AS Anio,
                       horas AS Horas, monto AS Monto, iva AS Iva, total AS Total,
                       observaciones AS Observaciones, porcentaje_cobro AS PorcentajeCobro
                FROM desglosesolicitud
                WHERE id_solicitud = @IdSolicitud
                ORDER BY anio DESC, mes DESC
                LIMIT @PageSize OFFSET @Offset;";

            using var connection = _connectionFactory.CreateConnection();
            int total = await connection.ExecuteScalarAsync<int>(countSql, new { IdSolicitud = idSolicitud });
            var items = await connection.QueryAsync<DesgloseSolicitud>(itemsSql, new
            {
                IdSolicitud = idSolicitud,
                PageSize = pageSize,
                Offset = (pageNumber - 1) * pageSize
            });
            return (items, total);
        }

        public async Task<DesgloseSolicitud?> GetByIdAsync(int idSolicitud, int mes, int anio)
        {
            const string sql = @"
                SELECT id_solicitud AS IdSolicitud, mes AS Mes, anio AS Anio,
                       horas AS Horas, monto AS Monto, iva AS Iva, total AS Total,
                       observaciones AS Observaciones, porcentaje_cobro AS PorcentajeCobro
                FROM desglosesolicitud
                WHERE id_solicitud = @IdSolicitud AND mes = @Mes AND anio = @Anio;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<DesgloseSolicitud>(sql, new { IdSolicitud = idSolicitud, Mes = mes, Anio = anio });
        }

        public async Task<bool> ExistsAsync(int idSolicitud, int mes, int anio, int? originalMes = null, int? originalAnio = null)
        {
            const string sql = @"
                SELECT COUNT(*) FROM desglosesolicitud
                WHERE id_solicitud = @IdSolicitud AND mes = @Mes AND anio = @Anio
                  AND (@OriginalMes IS NULL OR mes <> @OriginalMes OR anio <> @OriginalAnio);";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                IdSolicitud = idSolicitud,
                Mes = mes,
                Anio = anio,
                OriginalMes = originalMes,
                OriginalAnio = originalAnio
            }) > 0;
        }

        public async Task CreateAsync(DesgloseSolicitud desglose)
        {
            const string sql = @"
                INSERT INTO desglosesolicitud
                    (id_solicitud, mes, anio, horas, monto, iva, total, observaciones, porcentaje_cobro)
                VALUES
                    (@IdSolicitud, @Mes, @Anio, @Horas, @Monto, @Iva, @Total, @Observaciones, @PorcentajeCobro);";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, desglose);
        }

        public async Task<bool> UpdateAsync(DesgloseSolicitud desglose, int originalMes, int originalAnio)
        {
            const string sql = @"
                UPDATE desglosesolicitud SET
                    mes = @Mes, anio = @Anio, horas = @Horas, monto = @Monto,
                    iva = @Iva, total = @Total, observaciones = @Observaciones,
                    porcentaje_cobro = @PorcentajeCobro
                WHERE id_solicitud = @IdSolicitud
                  AND mes = @OriginalMes AND anio = @OriginalAnio;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(sql, new
            {
                desglose.IdSolicitud,
                desglose.Mes,
                desglose.Anio,
                desglose.Horas,
                desglose.Monto,
                desglose.Iva,
                desglose.Total,
                desglose.Observaciones,
                desglose.PorcentajeCobro,
                OriginalMes = originalMes,
                OriginalAnio = originalAnio
            }) > 0;
        }

        public async Task<bool> DeleteAsync(int idSolicitud, int mes, int anio)
        {
            const string sql = @"
                DELETE FROM desglosesolicitud
                WHERE id_solicitud = @IdSolicitud AND mes = @Mes AND anio = @Anio;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteAsync(sql, new { IdSolicitud = idSolicitud, Mes = mes, Anio = anio }) > 0;
        }
    }
}
