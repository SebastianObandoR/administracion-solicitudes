using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;
using System.Text.Json;

namespace AdministracionSolicitudes.Services.Implementations
{
    // Nota: Esto es temporal, ver IBitacoraService.cs. Luego se reemplaza por la version ya existente
    public class BitacoraService : IBitacoraService
    {
        private const string AccionCreate = "CREATE";
        private const string AccionUpdate = "UPDATE";
        private const string AccionDelete = "DELETE";
        private const string AccionConsult = "CONSULT";

        private readonly IBitacoraRepository _bitacoraRepository;

        public BitacoraService(IBitacoraRepository bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository;
        }

        public Task RegistrarCreacionAsync(int? idUsuario, string modulo, object registroNuevo)
        {
            string descripcion = JsonSerializer.Serialize(registroNuevo);
            return _bitacoraRepository.InsertAsync(idUsuario, modulo, AccionCreate, descripcion, DateTime.Now);
        }

        public Task RegistrarActualizacionAsync(int? idUsuario, string modulo, object registroAnterior, object registroActual)
        {
            var payload = new { Anterior = registroAnterior, Actual = registroActual };
            string descripcion = JsonSerializer.Serialize(payload);
            return _bitacoraRepository.InsertAsync(idUsuario, modulo, AccionUpdate, descripcion, DateTime.Now);
        }

        public Task RegistrarEliminacionAsync(int? idUsuario, string modulo, object registroEliminado)
        {
            string descripcion = JsonSerializer.Serialize(registroEliminado);
            return _bitacoraRepository.InsertAsync(idUsuario, modulo, AccionDelete, descripcion, DateTime.Now);
        }

        public Task RegistrarConsultaAsync(int? idUsuario, string modulo, string elemento)
        {
            string descripcion = $"El usuario consulta {elemento}";
            return _bitacoraRepository.InsertAsync(idUsuario, modulo, AccionConsult, descripcion, DateTime.Now);
        }
    }
}
