using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class ReporteFacturacionRepository : IReporteFacturacionRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public ReporteFacturacionRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

        public async Task<IEnumerable<ReporteFacturacionMensual>> GetMensualAsync(int mes, int anio)
        {
            const string sql = @"
                SELECT s.consecutivo_oficio AS ConsecutivoOficio,
                       s.titulo AS Titulo,
                       d.mes AS Mes,
                       d.anio AS Anio,
                       d.monto AS Monto,
                       d.iva AS Iva,
                       d.total AS Total
                FROM desglosesolicitud d
                INNER JOIN solicitud s ON s.id_solicitud = d.id_solicitud
                WHERE d.mes = @Mes AND d.anio = @Anio
                ORDER BY s.consecutivo_oficio;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<ReporteFacturacionMensual>(sql, new { Mes = mes, Anio = anio });
        }
    }
}
