using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Interfaces;
using Dapper;

namespace AdministracionSolicitudes.Repository.Implementations
{
    public class DiaFeriadoRepository : IDiaFeriadoRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DiaFeriadoRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<DateTime>> GetFechasAsync()
        {
            const string sql = "SELECT fecha FROM DiaFeriado;";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryAsync<DateTime>(sql);
        }
    }
}
