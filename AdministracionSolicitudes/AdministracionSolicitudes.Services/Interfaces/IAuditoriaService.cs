using System;
using System.Collections.Generic;
using System.Text;

namespace AdministracionSolicitudes.Services.Interfaces
{
    public interface IAuditoriaService
    {
        Task RegistrarCreacionAsync(
            int idUsuario,
            string modulo,
            object registro
        );

        Task RegistrarActualizacionAsync(
            int idUsuario,
            string modulo,
            object registroAnterior,
            object registroActual
        );

        Task RegistrarEliminacionAsync(
            int idUsuario,
            string modulo,
            object registroEliminado
        );

        Task RegistrarConsultaAsync(
            int idUsuario,
            string elemento
        );

        Task RegistrarErrorAsync(
            int? idUsuario,
            string modulo,
            Exception exception
        );
    }
}
