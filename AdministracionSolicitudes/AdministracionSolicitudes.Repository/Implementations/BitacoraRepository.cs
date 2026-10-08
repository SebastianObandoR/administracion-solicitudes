using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BitacoraRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task InsertAsync(int? idUsuario, string modulo, string accion, string descripcion, DateTime fecha)
        {
            const string sql = @"
                INSERT INTO Bitacora (id_usuario, modulo, accion, descripcion, fecha)
                VALUES (@IdUsuario, @Modulo, @Accion, @Descripcion, @Fecha);";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario, Modulo = modulo, Accion = accion, Descripcion = descripcion, Fecha = fecha });
        }
    }
}
