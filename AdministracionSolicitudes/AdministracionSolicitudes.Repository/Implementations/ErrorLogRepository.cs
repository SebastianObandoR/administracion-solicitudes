using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ErrorLogRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task RegistrarAsync(int usuarioId, string modulo, string mensaje)
        {
            const string sql = @"INSERT INTO errorlog (id_usuario, modulo, mensaje_error, fecha)
                                 VALUES (@UsuarioId, @Modulo, @Mensaje, CURRENT_TIMESTAMP);";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { UsuarioId = usuarioId, Modulo = modulo, Mensaje = mensaje });
        }
    }
}
