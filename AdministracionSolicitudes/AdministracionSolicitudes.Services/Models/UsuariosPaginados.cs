using System;
using System.Collections.Generic;
using System.Text;
using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class UsuariosPaginados
    {
        public IEnumerable<Usuario> Usuarios { get; set; } = [];
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
    }
}