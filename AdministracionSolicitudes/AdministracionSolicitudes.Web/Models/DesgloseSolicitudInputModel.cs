using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AdministracionSolicitudes.Web.Models
{
    public class DesgloseSolicitudInputModel
    {
        public int IdSolicitud { get; set; }
        public int OriginalMes { get; set; }
        public int OriginalAnio { get; set; }

        [Range(1, 12, ErrorMessage = "El mes debe estar entre 1 y 12")]
        public int Mes { get; set; }

        [Range(1, 9999, ErrorMessage = "El año debe estar entre 1 y 9999")]
        public int Anio { get; set; } = DateTime.Today.Year;

        [Range(typeof(decimal), "0", "999.99", ErrorMessage = "Las horas deben estar entre 0 y 999.99", ParseLimitsInInvariantCulture = true)]
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal Horas { get; set; }

        [Range(typeof(decimal), "0", "88495575.21", ErrorMessage = "El monto debe estar entre 0 y 88495575.21", ParseLimitsInInvariantCulture = true)]
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal Monto { get; set; }

        [StringLength(500, ErrorMessage = "Las observaciones no pueden superar 500 caracteres")]
        public string? Observaciones { get; set; }

        [Range(typeof(decimal), "0", "100", ErrorMessage = "El porcentaje de cobro debe estar entre 0 y 100", ParseLimitsInInvariantCulture = true)]
        [ModelBinder(BinderType = typeof(DecimalModelBinder))]
        public decimal PorcentajeCobro { get; set; }
    }
}
