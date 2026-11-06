using AdministracionSolicitudes.Web.Constants;

namespace AdministracionSolicitudes.Web.Middleware
{
    public class SessionValidationMiddleware
    {
        private const string LoginPath = "/Cuenta/Login";
        private const string ErrorPath = "/Error";
        private const string LoginRequiredMessage = "Por favor inicie sesi\u00F3n para utilizar el sistema";
        private readonly RequestDelegate _next;

        public SessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value ?? string.Empty;

            if (IsPublicPath(path) || context.Session.GetInt32(SessionKeys.UserId).HasValue)
            {
                await _next(context);
                return;
            }

            string message = Uri.EscapeDataString(LoginRequiredMessage);
            context.Response.Redirect($"{LoginPath}?message={message}");
        }

        private static bool IsPublicPath(string path)
        {
            return path.StartsWith(LoginPath, StringComparison.OrdinalIgnoreCase) || path.StartsWith(ErrorPath, StringComparison.OrdinalIgnoreCase);
        }
    }
}