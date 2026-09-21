using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private const int MaxFailedAttempts = 3;
        private const string InvalidCredentialsMessage = "Usuario y/o contrase\u00F1a incorrectos.";
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public AuthenticationService(IUserRepository userRepository, IPasswordEncryptionService passwordEncryptionService)
        {
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) return InvalidCredentials();

            var user = await _userRepository.GetByUsernameAsync(username.Trim());

            if (user == null || user.Estado != EstadoUsuario.Activo) return InvalidCredentials();

            bool validPassword = _passwordEncryptionService.Verify(password, user.ContrasenaCifrada, user.NonceContrasena, user.TagContrasena);

            if (!validPassword) return await HandleFailedAttemptAsync(user);

            if (user.IntentosFallidos > 0) await _userRepository.ResetFailedAttemptsAsync(user.IdUsuario);

            user.IntentosFallidos = 0;

            return new AuthenticationResult { IsSuccess = true, User = user };
        }

        private async Task<AuthenticationResult> HandleFailedAttemptAsync(Usuario user)
        {
            int failedAttempts = user.IntentosFallidos + 1;

            if (failedAttempts >= MaxFailedAttempts) await _userRepository.BlockAsync(user.IdUsuario);
            else await _userRepository.UpdateFailedAttemptsAsync(user.IdUsuario, failedAttempts);

            return InvalidCredentials();
        }

        private static AuthenticationResult InvalidCredentials()
        {
            return new AuthenticationResult { IsSuccess = false, Message = InvalidCredentialsMessage };
        }
    }
}
