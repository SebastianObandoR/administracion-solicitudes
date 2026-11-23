using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AdministracionSolicitudes.API.Repository;
using AdministracionSolicitudes.Repository.Implementations;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Implementations;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Services.Models;
using SharedConnectionFactory = AdministracionSolicitudes.Repository.Data.IDbConnectionFactory;

namespace AdministracionSolicitudes.API
{
    public static class RepresentanteEndpoint
    {
        private const string Ruta = "/api/representantes";
        private const string NoEncontrado = "No se encontró el representante seleccionado.";

        public static void AddRepresentantes(this IServiceCollection services)
        {
            services.AddSingleton<SharedConnectionFactory>(provider => provider.GetRequiredService<DbConnectionFactory>());
            services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<IRepresentanteService, RepresentanteService>();
        }

        public static void MapRepresentanteEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup(Ruta)
                .WithTags("Representantes")
                .RequireAuthorization(policy => policy.RequireAuthenticatedUser().RequireAssertion(context =>
                    int.TryParse(context.User.FindFirstValue(JwtRegisteredClaimNames.Sub), out var id) && id > 0));

            group.MapGet("", async (int? pagina, int? idRepresentante, ClaimsPrincipal usuario, IRepresentanteService service) =>
                Results.Ok(await service.ObtenerPaginaAsync(pagina ?? 1, UsuarioId(usuario), idRepresentante)));

            group.MapGet("/{id:int}", async (int id, ClaimsPrincipal usuario, IRepresentanteService service) =>
            {
                var representante = await service.ObtenerPorIdAsync(id, UsuarioId(usuario));
                return representante is null
                    ? Results.NotFound(new { mensaje = NoEncontrado })
                    : Results.Ok(representante);
            });

            group.MapPost("", async (RepresentanteInputModel input, ClaimsPrincipal usuario, IRepresentanteService service) =>
            {
                var (representante, errores) = await service.GuardarAsync(input, UsuarioId(usuario));
                return representante is null
                    ? Results.ValidationProblem(errores)
                    : Results.Created($"{Ruta}/{representante.IdRepresentante}", new
                    {
                        mensaje = "Representante creado correctamente.",
                        representante
                    });
            });

            group.MapPut("/{id:int}", async (int id, RepresentanteInputModel input, ClaimsPrincipal usuario, IRepresentanteService service) =>
            {
                var (representante, errores) = await service.GuardarAsync(input, UsuarioId(usuario), id);
                if (errores.Count > 0)
                {
                    return Results.ValidationProblem(errores);
                }

                return representante is null
                    ? Results.NotFound(new { mensaje = NoEncontrado })
                    : Results.Ok(new { mensaje = "Representante actualizado correctamente.", representante });
            });

            group.MapDelete("/{id:int}", async (int id, ClaimsPrincipal usuario, IRepresentanteService service) =>
            {
                var resultado = await service.EliminarAsync(id, UsuarioId(usuario));
                return resultado switch
                {
                    EliminarRepresentanteResultado.Eliminado => Results.Ok(new { mensaje = "Representante eliminado correctamente." }),
                    EliminarRepresentanteResultado.TieneSolicitudes => Results.Conflict(new
                    {
                        mensaje = "No se puede eliminar un registro con datos relacionados."
                    }),
                    _ => Results.NotFound(new { mensaje = NoEncontrado })
                };
            });
        }

        private static int UsuarioId(ClaimsPrincipal usuario) =>
            int.Parse(usuario.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
    }
}
