using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IBitacoraRepository _bitacoraRepository;
        private readonly IErrorLogRepository _errorLogRepository;

        private static readonly HashSet<string> PropiedadesSensibles = new(StringComparer.OrdinalIgnoreCase)
        {
            "Contrasena", "ContrasenaCifrada", "NonceContrasena", "TagContrasena", "Password"
        };

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        public AuditoriaService(IBitacoraRepository bitacoraRepository, IErrorLogRepository errorLogRepository)
        {
            _bitacoraRepository = bitacoraRepository;
            _errorLogRepository = errorLogRepository;
        }

        public async Task RegistrarCreacionAsync(int idUsuario, string modulo, object registro)
        {
            var json = SerializarSeguro(registro);
            await RegistrarBitacoraAsync(idUsuario, modulo, "CREATE", $"CREATE {json}");
        }

        public async Task RegistrarActualizacionAsync(int idUsuario, string modulo, object registroAnterior, object registroActual)
        {
            var datos = new { anterior = ObtenerNodoSeguro(registroAnterior), actual = ObtenerNodoSeguro(registroActual) };
            var json = JsonSerializer.Serialize(datos, JsonOptions);
            await RegistrarBitacoraAsync(idUsuario, modulo, "UPDATE", $"UPDATE {json}");
        }

        public async Task RegistrarEliminacionAsync(int idUsuario, string modulo, object registroEliminado)
        {
            var json = SerializarSeguro(registroEliminado);
            await RegistrarBitacoraAsync(idUsuario, modulo, "DELETE", $"DELETE {json}");
        }

        public async Task RegistrarConsultaAsync(int idUsuario, string elemento)
        {
            await RegistrarConsultaAsync(idUsuario, elemento, elemento);
        }

        public async Task RegistrarConsultaAsync(int idUsuario, string modulo, string elemento)
        {
            await RegistrarBitacoraAsync(idUsuario, modulo, "CONSULT", $"El usuario consulta {elemento}");
        }

        public async Task RegistrarErrorAsync(int? idUsuario, string modulo, Exception exception)
        {
            var errorLog = new ErrorLog
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                MensajeError = exception.Message
            };
            await _errorLogRepository.RegistrarAsync(errorLog);
        }

        private async Task RegistrarBitacoraAsync(int idUsuario, string modulo, string accion, string descripcion)
        {
            var bitacora = new Bitacora
            {
                IdUsuario = idUsuario,
                Modulo = modulo,
                Accion = accion,
                Descripcion = descripcion
            };
            await _bitacoraRepository.RegistrarAsync(bitacora);
        }

        private static string SerializarSeguro(object registro)
        {
            var nodo = ObtenerNodoSeguro(registro);
            return nodo?.ToJsonString(JsonOptions) ?? "{}";
        }

        private static JsonNode? ObtenerNodoSeguro(object registro)
        {
            var nodo = JsonSerializer.SerializeToNode(registro, JsonOptions);
            EliminarPropiedadesSensibles(nodo);
            return nodo;
        }

        private static void EliminarPropiedadesSensibles(JsonNode? nodo)
        {
            if (nodo is JsonObject objeto)
            {
                var propiedades = objeto.Select(propiedad => propiedad.Key).ToList();
                foreach (var propiedad in propiedades)
                {
                    if (PropiedadesSensibles.Contains(propiedad))
                    {
                        objeto.Remove(propiedad);
                        continue;
                    }
                    EliminarPropiedadesSensibles(objeto[propiedad]);
                }
                return;
            }

            if (nodo is JsonArray arreglo)
            {
                foreach (var elemento in arreglo) EliminarPropiedadesSensibles(elemento);
            }
        }
    }
}