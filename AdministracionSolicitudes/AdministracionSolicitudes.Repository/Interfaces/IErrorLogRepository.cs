using AdministracionSolicitudes.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Repository.Interfaces
{
    public interface IErrorLogRepository
    {
        Task RegistrarAsync(ErrorLog errorLog);
    }
}
