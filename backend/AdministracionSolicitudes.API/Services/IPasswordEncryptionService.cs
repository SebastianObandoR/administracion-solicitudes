
namespace AdministracionSolicitudes.API.Services
{
    public interface IPasswordEncryptionService
    {
        bool Verify(string password, string encryptedPassword, string nonce, string tag);
    }
}
