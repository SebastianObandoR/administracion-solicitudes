using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class ReporteTareasUsuarioRepository : IReporteTareasUsuarioRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ReporteTareasUsuarioRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TareaReporte>> ObtenerPorUsuarioMesAsync(int idUsuario, DateTime desde, DateTime hasta)
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
                WHERE t.id_usuario = @IdUsuario
                  AND t.fecha >= @Desde
                  AND t.fecha < @Hasta
                ORDER BY t.fecha, t.id_tarea;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<TareaReporte>(sql, new { IdUsuario = idUsuario, Desde = desde, Hasta = hasta });
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            const string sql = @"
                SELECT id_usuario AS IdUsuario,
                       nombre_usuario AS NombreUsuario,
                       nombre_completo AS NombreCompleto
                FROM usuarios
                ORDER BY nombre_completo;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Usuario>(sql);
        }
    }
}