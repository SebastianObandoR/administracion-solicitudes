
using AdministracionSolicitudes.API.Entities;

namespace AdministracionSolicitudes.API.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(Usuario usuario);
    }
}
