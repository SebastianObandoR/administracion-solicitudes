
using AdministracionSolicitudes.API.Entities;
using Dapper;

namespace AdministracionSolicitudes.API.Repository
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
            const string sql = """
                SELECT
                    id_usuario AS IdUsuario,
                    nombre_usuario AS NombreUsuario,
                    nombre_completo AS NombreCompleto,
                    correo AS Correo,
                    contrasena_cifrada AS ContrasenaCifrada,
                    nonce_contrasena AS NonceContrasena,
                    tag_contrasena AS TagContrasena,
                    estado AS Estado,
                    intentos_fallidos AS IntentosFallidos,
                    fecha_creacion AS FechaCreacion,
                    fecha_modificacion AS FechaModificacion
                FROM usuarios
                WHERE nombre_usuario = @Username;
                """;

            using var connection = _connectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Usuario>(
                sql,
                new { Username = username }
            );
        }

        public async Task UpdateFailedAttemptsAsync(int idUsuario, int intentosFallidos)
        {
            const string sql = """
                UPDATE usuarios
                SET intentos_fallidos = @IntentosFallidos,
                    fecha_modificacion = NOW()
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                IntentosFallidos = intentosFallidos
            });
        }

        public async Task ResetFailedAttemptsAsync(int idUsuario)
        {
            const string sql = """
                UPDATE usuarios
                SET intentos_fallidos = 0,
                    fecha_modificacion = NOW()
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario
            });
        }

        public async Task BlockAsync(int idUsuario)
        {
            const string sql = """
                UPDATE usuarios
                SET estado = @Estado,
                    fecha_modificacion = NOW()
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                Estado = EstadoUsuario.Bloqueado
            });
        }
    }
}
