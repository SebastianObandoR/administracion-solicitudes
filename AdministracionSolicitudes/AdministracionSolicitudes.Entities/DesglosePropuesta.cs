namespace AdministracionSolicitudes.Entities
{
    public class DesglosePropuesta
    {
        public int IdParticipacion { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public decimal Horas { get; set; }
        public decimal Monto { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Observaciones { get; set; } = string.Empty;
        public decimal PorcentajeCobro { get; set; }
    }
}
