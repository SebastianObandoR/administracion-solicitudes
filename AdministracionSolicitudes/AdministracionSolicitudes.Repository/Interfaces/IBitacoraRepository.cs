using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IBitacoraRepository
    {
        Task InsertarBitacoraAsync(int idUsuario, string modulo, AccionBitacora accion, string descripcion);
        Task InsertarErrorAsync(int idUsuario, string modulo, string mensajeError);
    }
}