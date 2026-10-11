using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class ReporteTareasSolicitudRepository : IReporteTareasSolicitudRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReporteTareasSolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TareaReporte>> ObtenerPorSolicitudMesAsync(int idSolicitud, DateTime desde, DateTime hasta)
        {
            const string sql = @"
                SELECT u.nombre_completo AS NombreUsuario,
                       t.fecha AS Fecha,
                       t.descripcion AS Descripcion,
                       t.horas_invertidas AS HorasInvertidas,
                       s.consecutivo_oficio AS ConsecutivoOficio,
                       s.titulo AS TituloSolicitud
                FROM Tarea t
                INNER JOIN usuarios u ON u.id_usuario = t.id_usuario
                INNER JOIN Solicitud s ON s.id_solicitud = t.id_solicitud
                WHERE t.id_solicitud = @IdSolicitud
                  AND t.fecha >= @Desde
                  AND t.fecha < @Hasta
                ORDER BY t.fecha, t.id_tarea;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<TareaReporte>(sql, new { IdSolicitud = idSolicitud, Desde = desde, Hasta = hasta });
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
