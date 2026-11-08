namespace AdministracionSolicitudes.WebService.AccesoDatos
{
    public interface IErrorImportacionRepositorio
    {
        void Registrar(string mensaje, int? idUsuario);
    }
}
