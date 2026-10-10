using AdministracionSolicitudes.Entities;
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

        public async Task InsertarBitacoraAsync(int idUsuario, string modulo, AccionBitacora accion, string descripcion)
        {
            const string sql = @"
                INSERT INTO Bitacora (id_usuario, modulo, accion, descripcion, fecha)
                VALUES (@IdUsuario, @Modulo, @Accion, @Descripcion, CURRENT_TIMESTAMP);";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                Accion = accion.ToString().ToUpperInvariant(),
                Descripcion = descripcion
            });
        }

        public async Task InsertarErrorAsync(int idUsuario, string modulo, string mensajeError)
        {
            const string sql = @"
                INSERT INTO ErrorLog (id_usuario, modulo, mensaje_error, fecha)
                VALUES (@IdUsuario, @Modulo, @MensajeError, CURRENT_TIMESTAMP);";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario, Modulo = modulo, MensajeError = mensajeError });
        }
    }
}