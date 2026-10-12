using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class EstadoSolicitudPagina
    {
        public IReadOnlyList<EstadoSolicitud> Estados { get; init; } = Array.Empty<EstadoSolicitud>();
        public int Pagina { get; init; }
        public int TamanoPagina { get; init; }
        public int Total { get; init; }
        public int TotalPaginas => (int)Math.Ceiling((double)Total / TamanoPagina);
    }
}
