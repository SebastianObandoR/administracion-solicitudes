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

        public async Task RegistrarAsync(Bitacora bitacora)
        {
            const string sql = """
                INSERT INTO bitacora (id_usuario, modulo, accion, descripcion, fecha)
                VALUES (@IdUsuario, @Modulo, @Accion, @Descripcion, NOW());
                """;

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, bitacora);
        }
    }
}