using AdministracionSolicitudes.Entities;
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

        public async Task RegistrarAsync(ErrorLog errorLog)
        {
            const string sql = """
                INSERT INTO error_log
                (
                    id_usuario,
                    modulo,
                    mensaje_error
                )
                VALUES
                (
                    @IdUsuario,
                    @Modulo,
                    @MensajeError
                );
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, errorLog);
        }
    }
}