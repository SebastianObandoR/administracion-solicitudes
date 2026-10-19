using System.ComponentModel.DataAnnotations;
using System.Globalization;
using AdministracionSolicitudes.Services.Models;

namespace AdministracionSolicitudes.Web.Models
{
    public class SolicitudInputModel
    {
        public const int AnioMinimo = SolicitudValidacion.AnioMinimo;
        public const int AnioMaximo = SolicitudValidacion.AnioMaximo;

        private const string FormatoHtml = "yyyy-MM-dd";

        public static string FechaMinimaHtml => new DateTime(AnioMinimo, 1, 1).ToString(FormatoHtml, CultureInfo.InvariantCulture);
        public static string FechaMaximaHtml => new DateTime(AnioMaximo, 12, 31).ToString(FormatoHtml, CultureInfo.InvariantCulture);
        public static string HoyHtml => DateTime.Today.ToString(FormatoHtml, CultureInfo.InvariantCulture);

        public int IdSolicitud { get; set; }

        public string? ConsecutivoOficio { get; set; } = string.Empty;

        public string? Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string? Observaciones { get; set; }

        public string? DocumentoInicio { get; set; }

        public string? DocumentoRespuesta { get; set; }

        public int? IdRepresentante { get; set; }

        public int? IdEstado { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaIngreso { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaRespuesta { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

    }
}
