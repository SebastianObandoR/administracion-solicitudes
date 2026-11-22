
using AdministracionSolicitudes.API.Entities;

namespace AdministracionSolicitudes.API.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
    }
}
