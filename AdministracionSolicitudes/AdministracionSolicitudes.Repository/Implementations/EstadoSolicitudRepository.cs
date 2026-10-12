using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    // SIMULACION SOL4
    public class EstadoSolicitudRepository : IEstadoSolicitudRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public EstadoSolicitudRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<EstadoSolicitud>> GetAllAsync()
        {
            const string sql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                  FROM EstadoSolicitud
                                  ORDER BY id_estado;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<EstadoSolicitud>(sql);
        }

        public async Task<EstadoSolicitud?> GetByIdAsync(int idEstado)
        {
            const string sql = @"SELECT id_estado AS IdEstado, nombre AS Nombre
                                  FROM EstadoSolicitud
                                  WHERE id_estado = @IdEstado;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<EstadoSolicitud>(sql, new { IdEstado = idEstado });
        }
    }
}
