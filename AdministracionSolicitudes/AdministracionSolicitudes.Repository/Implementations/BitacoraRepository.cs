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
                INSERT INTO Bitacora
                (
                    id_usuario,
                    modulo,
                    accion,
                    descripcion,
                    fecha
                )
                VALUES
                (
                    @IdUsuario,
                    @Modulo,
                    @Accion,
                    @Descripcion,
                    NOW()
                );
                """;

            string accion = bitacora.Accion switch
            {
                "CREAR" => "CREATE",
                "ACTUALIZAR" => "UPDATE",
                "ELIMINAR" => "DELETE",
                "CONSULTAR" => "CONSULT",
                _ => bitacora.Accion
            };

            using var connection = _connectionFactory.CreateConnection();

            await connection.ExecuteAsync(sql, new
            {
                bitacora.IdUsuario,
                bitacora.Modulo,
                Accion = accion,
                bitacora.Descripcion
            });
        }
    }
}