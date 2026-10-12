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
            return await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { Username = username });
        }

        public async Task<Usuario?> GetByIdAsync(int idUsuario)
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
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Usuario>(sql, new { IdUsuario = idUsuario });
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync(int pagina, int cantidad)
        {
            const string sql = """
                SELECT
                    id_usuario AS IdUsuario,
                    nombre_usuario AS NombreUsuario,
                    nombre_completo AS NombreCompleto,
                    correo AS Correo,
                    estado AS Estado,
                    intentos_fallidos AS IntentosFallidos,
                    fecha_creacion AS FechaCreacion,
                    fecha_modificacion AS FechaModificacion
                FROM usuarios
                ORDER BY nombre_usuario
                LIMIT @Cantidad OFFSET @Offset;
                """;

            var offset = (pagina - 1) * cantidad;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Usuario>(sql, new { Cantidad = cantidad, Offset = offset });
        }

        public async Task<int> CountAsync()
        {
            const string sql = "SELECT COUNT(*) FROM usuarios;";

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql);
        }

        public async Task<int> CreateAsync(Usuario usuario)
        {
            const string sql = """
                INSERT INTO usuarios
                (
                    nombre_usuario,
                    nombre_completo,
                    correo,
                    contrasena_cifrada,
                    nonce_contrasena,
                    tag_contrasena,
                    estado,
                    intentos_fallidos,
                    fecha_creacion
                )
                VALUES
                (
                    @NombreUsuario,
                    @NombreCompleto,
                    @Correo,
                    @ContrasenaCifrada,
                    @NonceContrasena,
                    @TagContrasena,
                    @Estado,
                    0,
                    NOW()
                );

                SELECT LAST_INSERT_ID();
                """;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(sql, usuario);
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            const string sql = """
                UPDATE usuarios
                SET
                    nombre_usuario = @NombreUsuario,
                    nombre_completo = @NombreCompleto,
                    correo = @Correo,
                    contrasena_cifrada = @ContrasenaCifrada,
                    nonce_contrasena = @NonceContrasena,
                    tag_contrasena = @TagContrasena,
                    estado = @Estado,
                    fecha_modificacion = NOW()
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, usuario);
        }

        public async Task DeleteAsync(int idUsuario)
        {
            const string sql = "DELETE FROM usuarios WHERE id_usuario = @IdUsuario;";

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario });
        }

        public async Task UpdateStatusAsync(int idUsuario, EstadoUsuario estado)
        {
            const string sql = """
                UPDATE usuarios
                SET estado = @Estado,
                    fecha_modificacion = NOW()
                WHERE id_usuario = @IdUsuario;
                """;

            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario, Estado = estado });
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
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario, IntentosFallidos = intentosFallidos });
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
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario });
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
            await connection.ExecuteAsync(sql, new { IdUsuario = idUsuario, Estado = EstadoUsuario.Bloqueado });
        }

        public async Task<bool> HasRelatedDataAsync(int idUsuario)
        {
            const string sql = """
            SELECT
                EXISTS(SELECT 1 FROM tarea WHERE id_usuario = @IdUsuario)
                OR EXISTS(SELECT 1 FROM historialestadosolicitud WHERE id_usuario = @IdUsuario);
            """;

            using var connection = _connectionFactory.CreateConnection();
            return await connection.ExecuteScalarAsync<bool>(sql, new { IdUsuario = idUsuario });
        }
    }
}