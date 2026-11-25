
using AdministracionSolicitudes.API;
using AdministracionSolicitudes.API.Repository;
using AdministracionSolicitudes.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = Convert.FromBase64String(
    builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Clave JWT no configurada.")
);

if (jwtKey.Length < 32)
{
    throw new InvalidOperationException("La clave JWT debe tener al menos 32 bytes.");
}

builder.Services.AddOpenApi();

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEstadoSolicitudRepository, EstadoSolicitudRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IBitacoraService, BitacoraService>();

builder.Services.AddScoped<IEstadoSolicitudRepository, EstadoSolicitudRepository>();
builder.Services.AddScoped<IEstadoSolicitudService, EstadoSolicitudService>();


builder.Services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
builder.Services.AddScoped<IRepresentanteService, RepresentanteService>();


builder.Services.AddSingleton<IPasswordEncryptionService, PasswordEncryptionService>();
builder.Services.AddSingleton<IJwtService, JwtService>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("React", policy =>
    {
        policy.WithOrigins(
            "http://127.0.0.1:3000",
            "http://localhost:3000",
            "https://tiusr21pl.cuc-carrera-ti.ac.cr"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapGet("/api/test-db", (IDbConnectionFactory factory) =>
    {
        try
        {
            using var connection = factory.CreateConnection();
            connection.Open();

            return Results.Ok(new
            {
                mensaje = "Conexion a MySQL exitosa"
            });
        }
        catch
        {
            return Results.Problem(
                "Error al conectar con MySQL",
                statusCode: 503
            );
        }
    });
}

app.UseHttpsRedirection();
app.UseCors("React");
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        mensaje = "API Administracion de Solicitudes funcionando correctamente",
        estado = "OK"
    });
})
.WithName("Inicio")
.WithTags("General");

app.MapLoginEndpoints();
app.MapEstadoSolicitudEndpoints();
app.MapRepresentanteEndpoints();

app.Run();
