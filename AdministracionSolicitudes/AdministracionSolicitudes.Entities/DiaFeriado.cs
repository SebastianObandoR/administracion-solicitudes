namespace AdministracionSolicitudes.Entities
{
    public class DiaFeriado
    {
        public int IdFeriado { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; } = string.Empty;
    }
}
