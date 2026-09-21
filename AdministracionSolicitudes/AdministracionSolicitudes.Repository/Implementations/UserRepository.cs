using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            const string sql = @"SELECT id_usuario AS IdUsuario, nombre_usuario AS NombreUsuario, nombre_completo AS NombreCompleto, correo AS Correo, contrasena_cifrada AS ContrasenaCifrada, nonce_contrasena AS NonceContrasena, tag_contrasena AS TagContrasena, estado AS Estado, intentos_fallidos AS IntentosFallidos, fecha_creacion AS FechaCreacion, fecha_modificacion AS FechaModificacion FROM usuarios WHERE nombre_usuario = @Username LIMIT 1;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Username = username });
        }

        public async Task UpdateFailedAttemptsAsync(int userId, int failedAttempts)
        {
            const string sql = @"UPDATE usuarios SET intentos_fallidos = @FailedAttempts, fecha_modificacion = CURRENT_TIMESTAMP WHERE id_usuario = @UserId;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { UserId = userId, FailedAttempts = failedAttempts });
        }

        public async Task ResetFailedAttemptsAsync(int userId)
        {
            const string sql = @"UPDATE usuarios SET intentos_fallidos = 0, fecha_modificacion = CURRENT_TIMESTAMP WHERE id_usuario = @UserId;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { UserId = userId });
        }

        public async Task BlockAsync(int userId)
        {
            const string sql = @"UPDATE usuarios SET estado = @BlockedStatus, intentos_fallidos = 3, fecha_modificacion = CURRENT_TIMESTAMP WHERE id_usuario = @UserId;";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { UserId = userId, BlockedStatus = (int)EstadoUsuario.Bloqueado });
        }
    }
}