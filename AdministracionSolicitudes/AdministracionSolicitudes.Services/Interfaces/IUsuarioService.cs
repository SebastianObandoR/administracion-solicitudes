using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuariosPaginados> ListarAsync(int pagina, int idUsuarioEjecutor);
        Task<Usuario?> ObtenerPorIdAsync(int idUsuario, int idUsuarioEjecutor);
        Task<ResultadoOperacion> CrearAsync(UsuarioEntrada entrada, int idUsuarioEjecutor);
        Task<ResultadoOperacion> ActualizarAsync(int idUsuario, UsuarioEntrada entrada, int idUsuarioEjecutor);
        Task<ResultadoOperacion> EliminarAsync(int idUsuario, int idUsuarioEjecutor);
        Task<ResultadoOperacion> CambiarEstadoAsync(int idUsuario, EstadoUsuario estado, int idUsuarioEjecutor);
    }
}