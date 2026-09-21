using AdministracionSolicitudes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IPasswordEncryptionService
    {
        PasswordEncryptionResult Encrypt(string password);
        bool Verify(string password, string encryptedPassword, string nonce, string tag);
    }
}
