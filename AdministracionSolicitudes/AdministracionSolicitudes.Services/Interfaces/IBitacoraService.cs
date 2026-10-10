using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IBitacoraService
    {
        Task RegistrarAsync(int idUsuario, string modulo, AccionBitacora accion, string descripcion);
        Task RegistrarErrorAsync(int idUsuario, string modulo, string contexto, Exception excepcion);
    }
}