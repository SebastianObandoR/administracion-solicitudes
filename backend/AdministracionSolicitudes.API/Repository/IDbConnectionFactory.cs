
using System.Data;

namespace AdministracionSolicitudes.API.Repository
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
