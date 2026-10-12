using AdministracionSolicitudes.Repository.Data;
using AdministracionSolicitudes.Repository.Implementations;
using AdministracionSolicitudes.Repository.Interfaces;
using AdministracionSolicitudes.Services.Implementations;
using AdministracionSolicitudes.Services.Interfaces;
using AdministracionSolicitudes.Web.Middleware;

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

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEstadoSolicitudRepository, EstadoSolicitudRepository>();
builder.Services.AddSingleton<IPasswordEncryptionService>(_ => new PasswordEncryptionService(encryptionKey));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IEstadoSolicitudService, EstadoSolicitudService>();

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