
using AdministracionSolicitudes.API.Entities;
using AdministracionSolicitudes.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AdministracionSolicitudes.API
{
    public static class LoginEndpoint
    {
        public static void MapLoginEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/auth")
                .WithTags("Autenticacion");

            group.MapPost("/login", async (
                LoginRequest request,
                IAuthenticationService authenticationService,
                IJwtService jwtService) =>
            {
                var result = await authenticationService.AuthenticateAsync(
                    request.Username,
                    request.Password
                );

                if (!result.IsSuccess || result.User == null)
                {
                    return Results.Json(
                        new { mensaje = result.Message },
                        statusCode: StatusCodes.Status401Unauthorized
                    );
                }

                var accessToken = jwtService.GenerateAccessToken(result.User);

                return Results.Ok(new LoginResponse
                {
                    AccessToken = accessToken
                });
            })
            .AllowAnonymous();

            group.MapGet("/me", (ClaimsPrincipal user) =>
            {
                return Results.Ok(new
                {
                    idUsuario = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value,
                    nombreUsuario = user.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value,
                    nombreCompleto = user.FindFirst("nombreCompleto")?.Value
                });
            })
            .RequireAuthorization();
        }
    }
}
