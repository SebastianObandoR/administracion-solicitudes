
using MySqlConnector;
using System.Data;
using SharedConnectionFactory = AdministracionSolicitudes.Repository.Data.IDbConnectionFactory;

namespace AdministracionSolicitudes.API.Repository
{
    public class DbConnectionFactory : IDbConnectionFactory, SharedConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(
                _configuration.GetConnectionString("DefaultConnection")
            );
        }
    }
}
