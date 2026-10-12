using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IReporteTareasUsuarioService
    {
        Task<ServiceResult<IReadOnlyList<Usuario>>> ObtenerUsuariosAsync(int idUsuarioSesion);
        Task<ServiceResult<ReporteTareasResultado>> GenerarReporteAsync(int idUsuario, int mes, int anio, int idUsuarioSesion);
    }
}
