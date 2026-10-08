using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class RepresentanteRepository : IRepresentanteRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public RepresentanteRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Representante>> GetAllAsync()
        {
            const string sql = @"SELECT id_representante AS IdRepresentante, nombre AS Nombre, correo AS Correo
                                  FROM Representante
                                  ORDER BY nombre;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<Representante>(sql);
        }
    }
}
