using System.Security.Claims;
using WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager;

namespace WebApplication1.Middleware.CustomMiddleware
{
    public class CustomAuthentification
    {
        private readonly RequestDelegate _next;
        private readonly ISessionService _sessionService;

        public CustomAuthentification(RequestDelegate next, ISessionService sessionService)
        {
            _next = next;
            _sessionService = sessionService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 🔹 Cas 1 : requête d'authentification
            if (context.Request.Path.StartsWithSegments("/api/Authentification", StringComparison.OrdinalIgnoreCase))
            {
                var claims = new List<Claim> { new Claim("Pass", "true") };
                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
                await _next(context);
                return;
            }

            // 🔹 Cas 2 : handshake WebSocket
            if (context.Request.Path == "/ws" && context.WebSockets.IsWebSocketRequest)
            {
                var token = context.Request.Query["token"].FirstOrDefault();

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Missing token for WebSocket");
                    return;
                }

                var session = _sessionService.GetSession(token);
                if (session == null)
                {
                    context.Response.StatusCode = 403;
                    await context.Response.WriteAsync("Invalid token for WebSocket");
                    return;
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, session.User.FullName),
                    new Claim("TokenValue", session.Token.Value),
                    new Claim("IsValidToken", "true")
                };
                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "WebSocket"));
                await _next(context);
                return;
            }

            // 🔹 Cas 3 : Requête HTTP standard avec Authorization header
            if (context.Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                var session = _sessionService.GetSession(tokenHeader);
                var claims = new List<Claim>();

                if (session != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, session.User.FullName));
                    claims.Add(new Claim("IsValidToken", "true"));
                    claims.Add(new Claim("TokenValue", session.Token.Value));
                }
                else
                {
                    claims.Add(new Claim("IsValidToken", "false"));
                }

                context.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
            }

            await _next(context);
        }
    }

    public static class CustomAuthentificationMiddleWareExtention
    {
        public static IApplicationBuilder UseCustomAuthentification(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthentification>();
        }
    }
}