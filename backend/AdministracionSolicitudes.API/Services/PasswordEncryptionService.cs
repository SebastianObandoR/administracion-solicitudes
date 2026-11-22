
using System.Security.Cryptography;
using System.Text;

namespace AdministracionSolicitudes.API.Services
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private const int TagSize = 16;
        private const int KeySize = 32;

        private readonly byte[] _key;

        public PasswordEncryptionService(IConfiguration configuration)
        {
            var encryptionKey = configuration["PasswordEncryption:Key"];

            if (string.IsNullOrWhiteSpace(encryptionKey))
                throw new InvalidOperationException("Clave AES no configurada.");

            _key = Convert.FromBase64String(encryptionKey);

            if (_key.Length != KeySize)
                throw new InvalidOperationException("La clave AES debe tener 32 bytes.");
        }

        public bool Verify(string password, string encryptedPassword, string nonce, string tag)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(encryptedPassword) ||
                string.IsNullOrWhiteSpace(nonce) ||
                string.IsNullOrWhiteSpace(tag))
                return false;

            try
            {
                byte[] cipherText = Convert.FromBase64String(encryptedPassword);
                byte[] nonceBytes = Convert.FromBase64String(nonce);
                byte[] tagBytes = Convert.FromBase64String(tag);
                byte[] plainText = new byte[cipherText.Length];
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                using var aes = new AesGcm(_key, TagSize);

                aes.Decrypt(nonceBytes, cipherText, tagBytes, plainText);

                return CryptographicOperations.FixedTimeEquals(plainText, passwordBytes);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (CryptographicException)
            {
                return false;
            }
        }
    }
}
