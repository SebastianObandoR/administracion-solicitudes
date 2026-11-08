using System;
using Dapper;
using MySql.Data.MySqlClient;

namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public class ErrorImportacionRepositorio : RepositorioImportacionBase, IErrorImportacionRepositorio
    {
        public ErrorImportacionRepositorio(string cadenaConexion)
            : base(cadenaConexion)
        {
        }

        public void Registrar(string mensaje, int? idUsuario)
        {
            using (var conexion = CrearConexion())
            {
                conexion.Execute(
                    @"INSERT INTO errorlog (id_usuario, modulo, mensaje_error, fecha)
                      VALUES (@IdUsuario, 'Carga de propuestas Excel', @Mensaje, NOW());",
                    new { IdUsuario = idUsuario, Mensaje = mensaje }
                );
            }
        }
    }
}
