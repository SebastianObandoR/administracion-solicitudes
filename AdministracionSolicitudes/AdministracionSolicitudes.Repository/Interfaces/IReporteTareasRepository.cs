using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IReporteTareasUsuarioRepository
    {
        Task<IEnumerable<TareaReporte>> ObtenerPorUsuarioMesAsync(int idUsuario, DateTime desde, DateTime hasta);
        Task<IEnumerable<Usuario>> ObtenerUsuariosAsync();
    }
}
