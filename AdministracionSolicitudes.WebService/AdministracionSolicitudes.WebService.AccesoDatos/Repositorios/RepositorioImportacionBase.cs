using System;
using MySql.Data.MySqlClient;
namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public abstract class RepositorioImportacionBase
    {
        protected string CadenaConexion { get; private set; }
        protected RepositorioImportacionBase(string cadenaConexion)
        {
            if (string.IsNullOrWhiteSpace(cadenaConexion)) throw new ArgumentException("La conexión es requerida.");
            CadenaConexion = cadenaConexion;
        }
        protected MySqlConnection CrearConexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
    }
}
