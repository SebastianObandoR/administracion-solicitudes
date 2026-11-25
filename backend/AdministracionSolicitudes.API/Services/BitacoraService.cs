
using AdministracionSolicitudes.API.Repository;
using System.Text.Json;

namespace AdministracionSolicitudes.API.Services
{
    public class BitacoraService : IBitacoraService
    {
        private readonly IBitacoraRepository _repository;

        public BitacoraService(IBitacoraRepository repository)
        {
            _repository = repository;
        }

        public Task RegistrarConsultaAsync(
            int idUsuario,
            string modulo,
            string elemento)
        {
            return _repository.RegistrarAsync(
                idUsuario,
                modulo,
                "CONSULT",
                $"El usuario consulta {elemento}");
        }

        public Task RegistrarCreacionAsync(
            int idUsuario,
            string modulo,
            object registro)
        {
            var descripcion = $"Se crea registro: {JsonSerializer.Serialize(registro)}";

            return _repository.RegistrarAsync(
                idUsuario,
                modulo,
                "CREATE",
                descripcion);
        }

        public Task RegistrarActualizacionAsync(
            int idUsuario,
            string modulo,
            object anterior,
            object actual)
        {
            var descripcion = JsonSerializer.Serialize(new
            {
                anterior,
                actual
            });

            return _repository.RegistrarAsync(
                idUsuario,
                modulo,
                "UPDATE",
                descripcion);
        }

        public Task RegistrarEliminacionAsync(
            int idUsuario,
            string modulo,
            object registro)
        {
            var descripcion = $"Se elimina registro: {JsonSerializer.Serialize(registro)}";

            return _repository.RegistrarAsync(
                idUsuario,
                modulo,
                "DELETE",
                descripcion);
        }

        public Task RegistrarAccionAsync(
            int idUsuario,
            string modulo,
            string accion,
            string descripcion)
        {
            return _repository.RegistrarAsync(
                idUsuario,
                modulo,
                accion,
                descripcion);
        }

        public Task RegistrarErrorAsync(
            int idUsuario,
            string modulo,
            Exception exception)
        {
            return _repository.RegistrarErrorAsync(
                idUsuario,
                modulo,
                exception.GetType().Name + ": " + exception.Message);
        }
    }
}
