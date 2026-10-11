using AdministracionSolicitudes.Entities;

namespace AdministracionSolicitudes.Services.Models
{
    public class RepresentantePagina
    {
        public IReadOnlyList<Representante> Representantes { get; init; } = Array.Empty<Representante>();
        public int Pagina { get; init; }
        public int TamanoPagina { get; init; }
        public int Total { get; init; }
        public int TotalPaginas => (int)Math.Ceiling((double)Total / TamanoPagina);
    }
}
