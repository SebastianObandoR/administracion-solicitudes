using AdministracionSolicitudes.WebService.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class SolicitudPublicadaDetalleAccesoDatos
    {
        private readonly string _connectionString;

        public SolicitudPublicadaDetalleAccesoDatos()
        {
            _connectionString =
                ConfigurationManager.ConnectionStrings["PublicaSA"]?.ConnectionString
                ?? throw new ConfigurationErrorsException(
                    "No se encontro la cadena de conexion PublicaSA");
        }

        public SolicitudPublicadaDetalle ObtenerDetalleSolicitudPublicada(int idSolicitud)
        {
            const string sql = @"
                SELECT
                    s.id_solicitud AS IdSolicitud,
                    s.consecutivo_oficio AS ConsecutivoOficio,
                    s.documento_respuesta AS DocumentoRespuesta,
                    s.documento_inicio AS DocumentoInicio,
                    s.titulo AS Titulo,
                    s.descripcion AS Descripcion,
                    r.nombre AS NombreRepresentante,
                    s.observaciones AS Observaciones,
                    e.nombre AS NombreEstado,
                    s.fecha_ingreso AS FechaIngreso,
                    s.fecha_respuesta AS FechaRespuesta,
                    s.fecha_inicio AS FechaInicio,
                    s.fecha_vencimiento_publicacion AS FechaVencimientoPublicacion,
                    s.publicada AS Publicada
                FROM solicitud s
                LEFT JOIN representante r
                    ON r.id_representante = s.id_representante
                LEFT JOIN estadosolicitud e
                    ON e.id_estado = s.id_estado
                WHERE s.id_solicitud = @IdSolicitud
                  AND s.publicada = 1
                  AND s.fecha_vencimiento_publicacion >= CURDATE()
                LIMIT 1;";

            using (var connection = new MySqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<SolicitudPublicadaDetalle>(
                    sql,
                    new { IdSolicitud = idSolicitud });
            }
        }
    }
}
