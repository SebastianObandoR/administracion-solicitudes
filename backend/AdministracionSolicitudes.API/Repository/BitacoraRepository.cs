
using Dapper;

namespace AdministracionSolicitudes.API.Repository
{
    public class BitacoraRepository : IBitacoraRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BitacoraRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task RegistrarAsync(
            int idUsuario,
            string modulo,
            string accion,
            string descripcion)
        {
            const string sql = """
                INSERT INTO bitacora
                (id_usuario, modulo, accion, descripcion, fecha)
                VALUES
                (@IdUsuario, @Modulo, @Accion, @Descripcion, NOW());
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                Accion = accion,
                Descripcion = descripcion
            });
        }

        public async Task RegistrarErrorAsync(
            int idUsuario,
            string modulo,
            string mensaje)
        {
            const string sql = """
                INSERT INTO errorlog
                (id_usuario, modulo, mensaje_error, fecha)
                VALUES
                (@IdUsuario, @Modulo, @Mensaje, NOW());
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                Mensaje = mensaje
            });
        }
    }
}
