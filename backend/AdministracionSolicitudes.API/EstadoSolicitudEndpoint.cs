
using System.Security.Claims;
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Repository;
using AdministracionSolicitudes.API.Services;

namespace AdministracionSolicitudes.API
{
    public static class EstadoSolicitudEndpoint
    {
        public static void MapEstadoSolicitudEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/estados")
                .WithTags("Estados")
                .RequireAuthorization();

            group.MapGet("/page", async (
                ClaimsPrincipal usuario,
                IEstadoSolicitudService service,
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
                IEstadoSolicitudService service) =>
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

                var estado = await service.ObtenerPorIdAsync(
                    id,
                    usuarioId
                );

                if (estado is null)
                {
                    return Results.NotFound(new
                    {
                        mensaje = "Estado no encontrado."
                    });
                }

                return Results.Ok(estado);
            });

            group.MapPost("", async (
                EstadoSolicitud input,
                ClaimsPrincipal usuario,
                IEstadoSolicitudService service) =>
            {
                if (!TryObtenerIdUsuario(usuario, out var usuarioId))
                    return Results.Unauthorized();

                var error = ValidarNombre(input);

                if (error is not null)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = error
                    });
                }

                var estado = new EstadoSolicitud
                {
                    Nombre = input.Nombre
                };

                var creado = await service.CrearAsync(
                    estado,
                    usuarioId
                );

                if (!creado)
                {
                    return Results.Conflict(new
                    {
                        mensaje = "Ya existe un estado con ese nombre."
                    });
                }

                return Results.Created(
                    $"/api/estados/{estado.IdEstado}",
                    estado
                );
            });

            group.MapPut("/{id:int}", async (
                int id,
                EstadoSolicitud input,
                ClaimsPrincipal usuario,
                IEstadoSolicitudService service) =>
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

                var error = ValidarNombre(input);

                if (error is not null)
                {
                    return Results.BadRequest(new
                    {
                        mensaje = error
                    });
                }

                var estado = new EstadoSolicitud
                {
                    IdEstado = id,
                    Nombre = input.Nombre
                };

                var resultado = await service.ActualizarAsync(
                    estado,
                    usuarioId
                );

                return resultado switch
                {
                    ActualizarEstadoSolicitudResultado.Actualizado =>
                        Results.Ok(estado),

                    ActualizarEstadoSolicitudResultado.NoEncontrado =>
                        Results.NotFound(new
                        {
                            mensaje = "Estado no encontrado."
                        }),

                    ActualizarEstadoSolicitudResultado.NombreDuplicado =>
                        Results.Conflict(new
                        {
                            mensaje = "Ya existe un estado con ese nombre."
                        }),

                    _ => Results.Problem(
                        "No se pudo actualizar el estado."
                    )
                };
            });

            group.MapDelete("/{id:int}", async (
                int id,
                ClaimsPrincipal usuario,
                IEstadoSolicitudService service) =>
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
                    EliminarEstadoSolicitudResultado.Eliminado =>
                        Results.NoContent(),

                    EliminarEstadoSolicitudResultado.NoEncontrado =>
                        Results.NotFound(new
                        {
                            mensaje = "Estado no encontrado."
                        }),

                    EliminarEstadoSolicitudResultado.TieneSolicitudes =>
                        Results.Conflict(new
                        {
                            mensaje = "El estado tiene solicitudes relacionadas y no puede eliminarse."
                        }),

                    _ => Results.Problem(
                        "No se pudo eliminar el estado."
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

        private static string? ValidarNombre(
            EstadoSolicitud estado)
        {
            estado.Nombre = estado.Nombre?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(estado.Nombre))
                return "El nombre es requerido.";

            if (estado.Nombre.Length > 50)
                return "El nombre no puede superar los 50 caracteres.";

            return null;
        }
    }
}
