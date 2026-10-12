namespace AdministracionSolicitudes.Services.Models
{
    public class SolicitudOperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? IdSolicitud { get; set; }

        public static SolicitudOperationResult Success(int? idSolicitud = null, string message = "")
            => new() { IsSuccess = true, IdSolicitud = idSolicitud, Message = message };

        public static SolicitudOperationResult Failure(string message)
            => new() { IsSuccess = false, Message = message };
    }
}
