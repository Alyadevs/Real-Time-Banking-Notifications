using Microsoft.AspNetCore.Builder;

namespace WebApplication1.Middleware.CustomMiddleware
{
    public static class CustomAuthentificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthentification(this IApplicationBuilder builder)
        {
            // Ajoutez ici la logique de votre middleware personnalisé, par exemple :
            // return builder.UseMiddleware<CustomAuthentificationMiddleware>();
            // Si vous avez déjà un middleware, décommentez la ligne ci-dessus et assurez-vous que la classe existe.
            return builder;
        }
    }
}