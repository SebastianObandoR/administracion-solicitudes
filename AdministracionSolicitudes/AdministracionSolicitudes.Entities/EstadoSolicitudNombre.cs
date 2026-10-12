namespace AdministracionSolicitudes.Entities
{
    // SIMULACION SOL4: ids de los estados por defecto. SOL1 y SOL5 dependen de que coincidan con la tabla EstadoSolicitud (1 a 5)
    public enum EstadoSolicitudNombre
    {
        Nueva = 1,
        Respondida = 2,
        Iniciada = 3,
        Finalizada = 4,
        Vencida = 5
    }
}
