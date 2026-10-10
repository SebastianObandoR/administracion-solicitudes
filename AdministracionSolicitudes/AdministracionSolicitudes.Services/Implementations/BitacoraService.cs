using AdministracionSolicitudes.Entities;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Interfaces;

namespace AdministracionSolicitudes.Services.Implementations
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _bitacoraRepository;

        public BitacoraService(IBitacoraRepository bitacoraRepository)
        {
            _bitacoraRepository = bitacoraRepository;
        }

        public async Task RegistrarAsync(int idUsuario, string modulo, AccionBitacora accion, string descripcion)
        {
            await _bitacoraRepository.InsertarBitacoraAsync(idUsuario, modulo, accion, descripcion);
        }

        public async Task RegistrarErrorAsync(int idUsuario, string modulo, string contexto, Exception excepcion)
        {
            try
            {
                await _bitacoraRepository.InsertarErrorAsync(idUsuario, modulo, $"{contexto}: {excepcion.Message}");
            }
            catch
            {
                
            }
        }
    }
}