using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using AdministracionSolicitudes.WebService.Modelos;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class ParticipacionesRepositorio : IParticipacionesRepositorio
    {
        private readonly string _cadenaConexion;

        public ParticipacionesRepositorio(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La conexión es requerida.");
            }

            _cadenaConexion = cadenaConexion;
        }

        public bool ExisteProveedor(string cedulaJuridica)
        {
            const string sql = @"
                SELECT COUNT(1)
                FROM proveedor
                WHERE REPLACE(REPLACE(cedula_juridica, '-', ''), ' ', '') = @Cedula;";

            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                return conexion.ExecuteScalar<int>(sql, new { Cedula = cedulaJuridica }) > 0;
            }
        }

        public List<SolicitudParticipacion> ObtenerSolicitudes(string cedulaJuridica)
        {
            const string sql = @"
                SELECT
                    s.consecutivo_oficio AS ConsecutivoOficio,
                    s.titulo AS Titulo
                FROM participacion p
                INNER JOIN proveedor pr
                    ON pr.id_proveedor = p.id_proveedor
                INNER JOIN solicitud s
                    ON s.id_solicitud = p.id_solicitud
                WHERE REPLACE(REPLACE(pr.cedula_juridica, '-', ''), ' ', '') = @Cedula
                ORDER BY s.id_solicitud DESC;";

            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                return conexion.Query<SolicitudParticipacion>(
                    sql,
                    new { Cedula = cedulaJuridica }
                ).ToList();
            }
        }
    }
}
