using System;
using Dapper;
using MySql.Data.MySqlClient;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class SeguimientoParticipacionesRepositorio : ISeguimientoParticipacionesRepositorio
    {
        private readonly string _cadenaConexion;

        public SeguimientoParticipacionesRepositorio(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                throw new ArgumentException("La conexión es requerida.");
            }

            _cadenaConexion = cadenaConexion;
        }

        public void RegistrarConsulta()
        {
            const string sql = @"
                INSERT INTO bitacora (id_usuario, modulo, accion, descripcion, fecha)
                VALUES (
                    NULL,
                    'Participaciones en solicitudes',
                    'CONSULT',
                    'El usuario consulta participaciones en solicitudes',
                    NOW()
                );";

            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Execute(sql);
            }
        }

        public void RegistrarError(string mensaje)
        {
            const string sql = @"
                INSERT INTO errorlog (id_usuario, modulo, mensaje_error, fecha)
                VALUES (NULL, 'Participaciones en solicitudes', @Mensaje, NOW());";

            using (var conexion = new MySqlConnection(_cadenaConexion))
            {
                conexion.Execute(sql, new { Mensaje = mensaje });
            }
        }
    }
}
