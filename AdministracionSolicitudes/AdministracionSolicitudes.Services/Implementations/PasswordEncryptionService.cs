using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        private const int NonceSize = 12;
        private const int TagSize = 16;
        private const int KeySize = 32;
        private readonly byte[] _key;

        public PasswordEncryptionService(string encryptionKey)
        {
            if (string.IsNullOrWhiteSpace(encryptionKey)) throw new ArgumentException("La clave de cifrado es requerida.", nameof(encryptionKey));

            _key = Convert.FromBase64String(encryptionKey);

            if (_key.Length != KeySize) throw new ArgumentException("La clave de cifrado debe contener 32 bytes para AES-256.", nameof(encryptionKey));
        }

        public PasswordEncryptionResult Encrypt(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("La contrasena es requerida.", nameof(password));

            byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
            byte[] plainText = Encoding.UTF8.GetBytes(password);
            byte[] cipherText = new byte[plainText.Length];
            byte[] tag = new byte[TagSize];

            using var aes = new AesGcm(_key, TagSize);
            aes.Encrypt(nonce, plainText, cipherText, tag);

            return new PasswordEncryptionResult
            {
                EncryptedPassword = Convert.ToBase64String(cipherText),
                Nonce = Convert.ToBase64String(nonce),
                Tag = Convert.ToBase64String(tag)
            };
        }

        public bool Verify(string password, string encryptedPassword, string nonce, string tag)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(encryptedPassword) || string.IsNullOrWhiteSpace(nonce) || string.IsNullOrWhiteSpace(tag)) return false;

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
