namespace AdministracionSolicitudes.Services.Models
{
    public class SolicitudOperationResult
    {
        public bool IsSuccess { get; set; }
        public bool HasWarning { get; set; }
        public string Message { get; set; } = string.Empty;
        public Dictionary<string, string[]> ValidationErrors { get; set; } = new();
        public int? IdSolicitud { get; set; }

        public static SolicitudOperationResult Success(int? idSolicitud = null, string message = "")
            => new() { IsSuccess = true, IdSolicitud = idSolicitud, Message = message };

        public static SolicitudOperationResult SuccessWithWarning(int? idSolicitud, string message)
            => new() { IsSuccess = true, HasWarning = true, IdSolicitud = idSolicitud, Message = message };

        public static SolicitudOperationResult Failure(string message)
            => new() { IsSuccess = false, Message = message };
    }
}
