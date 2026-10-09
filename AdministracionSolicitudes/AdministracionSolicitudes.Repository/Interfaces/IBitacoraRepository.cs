using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IBitacoraRepository
    {
        Task RegistrarAsync(Bitacora bitacora);
    }
}