using AdministracionSolicitudes.WebService.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class SolicitudPublicadaAccesoDatos
    {
        private readonly string _connectionString;

        public SolicitudPublicadaAccesoDatos()
        {
            _connectionString =
                ConfigurationManager.ConnectionStrings["PublicaSA"]?.ConnectionString
                ?? throw new ConfigurationErrorsException(
                    "No se encontro la cadena de conexion PublicaSA");
        }

        public List<SolicitudPublicada> ObtenerSolicitudesPublicadas()
        {
            const string sql = @"
                SELECT
                    s.id_solicitud AS IdSolicitud,
                    s.consecutivo_oficio AS ConsecutivoOficio,
                    s.titulo AS Titulo,
                    r.nombre AS NombreRepresentante
                FROM solicitud s
                LEFT JOIN representante r
                    ON r.id_representante = s.id_representante
                WHERE s.publicada = 1
                  AND s.fecha_vencimiento_publicacion >= CURDATE()
                ORDER BY s.id_solicitud DESC;";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection
                    .Query<SolicitudPublicada>(sql)
                    .ToList();
            }
        }
    }
}