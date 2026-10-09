using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private const int CantidadPorPagina = 10;
        private const string Modulo = "Usuarios";

        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptionService _passwordEncryptionService;
        private readonly IAuditoriaService _auditoriaService;

        public UsuarioService(IUserRepository userRepository, IPasswordEncryptionService passwordEncryptionService, IAuditoriaService auditoriaService)
        {
            _userRepository = userRepository;
            _passwordEncryptionService = passwordEncryptionService;
            _auditoriaService = auditoriaService;
        }

        public async Task<UsuariosPaginados> ListarAsync(int pagina, int idUsuarioEjecutor)
        {
            try
            {
                pagina = pagina < 1 ? 1 : pagina;

                var totalUsuarios = await _userRepository.CountAsync();
                var totalPaginas = (int)Math.Ceiling(totalUsuarios / (double)CantidadPorPagina);

                if (totalPaginas > 0 && pagina > totalPaginas) pagina = totalPaginas;

                var usuarios = await _userRepository.GetAllAsync(pagina, CantidadPorPagina);
                await _auditoriaService.RegistrarConsultaAsync(idUsuarioEjecutor, "usuarios");

                return new UsuariosPaginados
                {
                    Usuarios = usuarios,
                    PaginaActual = pagina,
                    TotalPaginas = totalPaginas
                };
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                throw;
            }
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int idUsuario, int idUsuarioEjecutor)
        {
            try
            {
                var usuario = await _userRepository.GetByIdAsync(idUsuario);
                await _auditoriaService.RegistrarConsultaAsync(idUsuarioEjecutor, "usuario");
                return usuario;
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                throw;
            }
        }

        public async Task<ResultadoOperacion> CrearAsync(UsuarioEntrada entrada, int idUsuarioEjecutor)
        {
            var validacion = ValidarEntrada(entrada);
            if (!validacion.Exitoso) return validacion;

            try
            {
                var cifrado = _passwordEncryptionService.Encrypt(entrada.Contrasena);

                var usuario = new Usuario
                {
                    NombreUsuario = entrada.NombreUsuario.Trim(),
                    NombreCompleto = entrada.NombreCompleto.Trim(),
                    Correo = entrada.Correo.Trim(),
                    ContrasenaCifrada = cifrado.EncryptedPassword,
                    NonceContrasena = cifrado.Nonce,
                    TagContrasena = cifrado.Tag,
                    Estado = entrada.Estado
                };

                usuario.IdUsuario = await _userRepository.CreateAsync(usuario);
                await _auditoriaService.RegistrarCreacionAsync(idUsuarioEjecutor, Modulo, usuario);

                return ResultadoOperacion.Exito("Usuario creado correctamente.");
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                return ResultadoOperacion.Error("Ocurrio un error al crear el usuario.");
            }
        }

        public async Task<ResultadoOperacion> ActualizarAsync(int idUsuario, UsuarioEntrada entrada, int idUsuarioEjecutor)
        {
            var validacion = ValidarEntrada(entrada);
            if (!validacion.Exitoso) return validacion;

            try
            {
                var usuarioAnterior = await _userRepository.GetByIdAsync(idUsuario);
                if (usuarioAnterior == null) return ResultadoOperacion.Error("Usuario no encontrado.");

                var cifrado = _passwordEncryptionService.Encrypt(entrada.Contrasena);

                var usuarioActual = new Usuario
                {
                    IdUsuario = idUsuario,
                    NombreUsuario = entrada.NombreUsuario.Trim(),
                    NombreCompleto = entrada.NombreCompleto.Trim(),
                    Correo = entrada.Correo.Trim(),
                    ContrasenaCifrada = cifrado.EncryptedPassword,
                    NonceContrasena = cifrado.Nonce,
                    TagContrasena = cifrado.Tag,
                    Estado = entrada.Estado,
                    IntentosFallidos = usuarioAnterior.IntentosFallidos,
                    FechaCreacion = usuarioAnterior.FechaCreacion
                };

                await _userRepository.UpdateAsync(usuarioActual);
                await _auditoriaService.RegistrarActualizacionAsync(idUsuarioEjecutor, Modulo, usuarioAnterior, usuarioActual);

                return ResultadoOperacion.Exito("Usuario actualizado correctamente.");
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                return ResultadoOperacion.Error("Ocurrio un error al actualizar el usuario.");
            }
        }

        public async Task<ResultadoOperacion> EliminarAsync(int idUsuario, int idUsuarioEjecutor)
        {
            try
            {
                var usuario = await _userRepository.GetByIdAsync(idUsuario);
                if (usuario == null) return ResultadoOperacion.Error("Usuario no encontrado.");

                await _userRepository.DeleteAsync(idUsuario);
                await _auditoriaService.RegistrarEliminacionAsync(idUsuarioEjecutor, Modulo, usuario);

                return ResultadoOperacion.Exito("Usuario eliminado correctamente.");
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                return ResultadoOperacion.Error("No se puede eliminar un registro con datos relacionados.");
            }
        }

        public async Task<ResultadoOperacion> CambiarEstadoAsync(int idUsuario, EstadoUsuario estado, int idUsuarioEjecutor)
        {
            if (!Enum.IsDefined(estado)) return ResultadoOperacion.Error("Estado de usuario invalido.");

            try
            {
                var usuarioAnterior = await _userRepository.GetByIdAsync(idUsuario);
                if (usuarioAnterior == null) return ResultadoOperacion.Error("Usuario no encontrado.");

                await _userRepository.UpdateStatusAsync(idUsuario, estado);

                var usuarioActual = await _userRepository.GetByIdAsync(idUsuario);
                if (usuarioActual != null) await _auditoriaService.RegistrarActualizacionAsync(idUsuarioEjecutor, Modulo, usuarioAnterior, usuarioActual);

                return ResultadoOperacion.Exito("Estado del usuario actualizado correctamente.");
            }
            catch (Exception exception)
            {
                await _auditoriaService.RegistrarErrorAsync(idUsuarioEjecutor, Modulo, exception);
                return ResultadoOperacion.Error("Ocurrio un error al actualizar el estado del usuario.");
            }
        }

        private static ResultadoOperacion ValidarEntrada(UsuarioEntrada entrada)
        {
            if (string.IsNullOrWhiteSpace(entrada.NombreUsuario) ||
                string.IsNullOrWhiteSpace(entrada.NombreCompleto) ||
                string.IsNullOrWhiteSpace(entrada.Correo) ||
                string.IsNullOrWhiteSpace(entrada.Contrasena) ||
                !Enum.IsDefined(entrada.Estado))
            {
                return ResultadoOperacion.Error("Todos los datos son requeridos.");
            }

            return ResultadoOperacion.Exito(string.Empty);
        }
    }
}
