
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;
using AdministracionSolicitudes.API.Services;

namespace AdministracionSolicitudes.API
{
    public static class RepresentanteEndpoint
    {
        public static void MapRepresentanteEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/representantes")
                .WithTags("Representantes")
                .RequireAuthorization();

            group.MapGet("/page", async (
                ClaimsPrincipal usuario,
                IRepresentanteService service,
                int pageNumber = 1,
                int pageSize = 10) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                if (pageNumber < 1)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = "El numero de pagina debe ser mayor que cero."
                    });
                }

                if (pageSize is < 1 or > 100)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = "El tamano de pagina debe estar entre 1 y 100."
                    });
                }

                var resultado = await service.ObtenerPaginaAsync(
                    pageNumber,
                    pageSize,
                    usuarioId
                );

                return Results.Ok(resultado);
            });

            group.MapGet("/{id:int}", async (
                int id,
                ClaimsPrincipal usuario,
                IRepresentanteService service) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                if (id <= 0)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = "Id invalido."
                    });
                }

                var representante = await service.ObtenerPorIdAsync(
                    id,
                    usuarioId
                );

                if (representante is null)
                {
                    return Results.NotFound(new
                    {
                        mensaje = "Representante no encontrado."
                    });
                }

                return Results.Ok(representante);
            });

            group.MapPost("", async (
                Representante input,
                ClaimsPrincipal usuario,
                IRepresentanteService service) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                var error = ValidarRepresentante(input);

                if (error is not null)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = error
                    });
                }

                var representante = new Representante
                {
                    Nombre = input.Nombre,
                    Correo = input.Correo
                };

                var creado = await service.CrearAsync(
                    representante,
                    usuarioId
                );

                if (!creado)
                {
                    return Results.Conflict(new
                    {
                        mensaje = "Ya existe un representante con ese correo."
                    });
                }

                return Results.Created(
                    $"/api/representantes/{representante.IdRepresentante}",
                    representante
                );
            });

            group.MapPut("/{id:int}", async (
                int id,
                Representante input,
                ClaimsPrincipal usuario,
                IRepresentanteService service) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                if (id <= 0)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = "Id invalido."
                    });
                }

                var error = ValidarRepresentante(input);

                if (error is not null)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = error
                    });
                }

                var representante = new Representante
                {
                    IdRepresentante = id,
                    Nombre = input.Nombre,
                    Correo = input.Correo
                };

                var resultado = await service.ActualizarAsync(
                    representante,
                    usuarioId
                );

                return resultado switch
                {
                    ActualizarRepresentanteResultado.Actualizado =>
                        Results.Ok(representante),

                    ActualizarRepresentanteResultado.NoEncontrado =>
                        Results.NotFound(new
                        {
                            mensaje = "Representante no encontrado."
                        }),

                    ActualizarRepresentanteResultado.CorreoDuplicado =>
                        Results.Conflict(new
                        {
                            mensaje = "Ya existe un representante con ese correo."
                        }),

                    _ => Results.Problem(
                        "No se pudo actualizar el representante."
                    )
                };
            });

            group.MapDelete("/{id:int}", async (
                int id,
                ClaimsPrincipal usuario,
                IRepresentanteService service) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                if (id <= 0)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = "Id invalido."
                    });
                }

                var resultado = await service.EliminarAsync(
                    id,
                    usuarioId
                );

                return resultado switch
                {
                    EliminarRepresentanteResultado.Eliminado =>
                        Results.NoContent(),

                    EliminarRepresentanteResultado.NoEncontrado =>
                        Results.NotFound(new
                        {
                            mensaje = "Representante no encontrado."
                        }),

                    EliminarRepresentanteResultado.TieneSolicitudes =>
                        Results.Conflict(new
                        {
                            mensaje = "El representante tiene solicitudes relacionadas y no puede eliminarse."
                        }),

                    _ => Results.Problem(
                        "No se pudo eliminar el representante."
                    )
                };
            });
        }

        private static bool TryObtenerIdUsuario(
            ClaimsPrincipal usuario,
            out int usuarioId)
        {
            return int.TryParse(
                usuario.FindFirst("sub")?.Value,
                out usuarioId
            ) && usuarioId > 0;
        }

        private static string? ValidarRepresentante(
            Representante representante)
        {
            representante.Nombre =
                representante.Nombre?.Trim() ?? string.Empty;

            representante.Correo =
                representante.Correo?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(representante.Nombre))
                return "El nombre es requerido.";

            if (representante.Nombre.Length > 150)
                return "El nombre no puede superar los 150 caracteres.";

            if (string.IsNullOrWhiteSpace(representante.Correo))
                return "El correo es requerido.";

            if (representante.Correo.Length > 150)
                return "El correo no puede superar los 150 caracteres.";

            if (!new EmailAddressAttribute().IsValid(representante.Correo))
                return "Ingrese un correo electronico valido.";

            return null;
        }
    }
}
