using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Implementations;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Implementations;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Middleware;
using AdministracionSolicitudes.Web.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No se encontro la cadena de conexion DefaultConnection");
var encryptionKey = builder.Configuration["SecuritySettings:EncryptionKey"] ?? throw new InvalidOperationException("No se encontro la clave de cifrado");
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<AdministracionSolicitudes.Services.Models.EmailSettings>() ?? new AdministracionSolicitudes.Services.Models.EmailSettings();

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddSingleton<IPasswordEncryptionService>(_ => new PasswordEncryptionService(encryptionKey));
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// SOL1
builder.Services.AddScoped<ISolicitudRepository, SolicitudRepository>();
builder.Services.AddScoped<IRepresentanteRepository, RepresentanteRepository>();
builder.Services.AddScoped<IEstadoSolicitudRepository, EstadoSolicitudRepository>();
builder.Services.AddScoped<IDiaFeriadoRepository, DiaFeriadoRepository>();
builder.Services.AddScoped<ISolicitudService, SolicitudService>();
builder.Services.AddScoped<IVencimientoService, VencimientoService>();

// SOL2
builder.Services.AddScoped<IDesgloseSolicitudRepository, DesgloseSolicitudRepository>();
builder.Services.AddScoped<IDesgloseSolicitudService, DesgloseSolicitudService>();

// SOL3
builder.Services.AddScoped<IRepresentanteService, RepresentanteService>();

// SOL4
builder.Services.AddScoped<IEstadoSolicitudService, EstadoSolicitudService>();

// SOL5
builder.Services.AddHostedService<VencimientoSolicitudesWorker>();

// SOL6
builder.Services.AddSingleton(emailSettings);
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INotificacionEstadoRepository, NotificacionEstadoRepository>();
builder.Services.AddScoped<INotificacionEstadoService, NotificacionEstadoService>();

// SOL7
builder.Services.AddScoped<IReporteFacturacionRepository, ReporteFacturacionRepository>();
builder.Services.AddScoped<IReporteFacturacionService, ReporteFacturacionService>();

// TAR1
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<ITareaService, TareaService>();


// TAR2
builder.Services.AddScoped<IReporteTareasUsuarioRepository, ReporteTareasUsuarioRepository>();
builder.Services.AddScoped<IReporteTareasUsuarioService, ReporteTareasUsuarioService>();

// TAR3
builder.Services.AddScoped<IReporteTareasSolicitudRepository, ReporteTareasSolicitudRepository>();
builder.Services.AddScoped<IReporteTareasSolicitudService, ReporteTareasSolicitudService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseMiddleware<SessionValidationMiddleware>();
app.UseAuthorization();
app.MapRazorPages();

app.Run();