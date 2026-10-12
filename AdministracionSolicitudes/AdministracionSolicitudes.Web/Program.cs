using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Implementations;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Implementations;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Middleware;
using System.Globalization;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

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

var keyBytes = Convert.FromBase64String(encryptionKey);
var huellaKey = Convert.ToHexString(SHA256.HashData(keyBytes));

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordEncryptionService>(_ => new PasswordEncryptionService(encryptionKey));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IBitacoraRepository, BitacoraRepository>();
builder.Services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<ITareaService, TareaService>();

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