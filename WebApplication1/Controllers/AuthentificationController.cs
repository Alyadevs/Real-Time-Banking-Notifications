using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager;
using WebApplication1.Models.Entites;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public AuthentificationController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] User credentials)
        {
            // ✅ LOGS DE DEBUG
            Console.WriteLine("======================================");
            Console.WriteLine("🔐 TENTATIVE D'AUTHENTIFICATION");
            Console.WriteLine($"📝 Login reçu: '{credentials?.Login}'");
            Console.WriteLine($"📝 Password reçu: '{credentials?.Password}'");
            Console.WriteLine("======================================");

            if (credentials == null)
            {
                Console.WriteLine("❌ Credentials NULL");
                return BadRequest("Credentials manquants");
            }

            if (string.IsNullOrEmpty(credentials.Login) || string.IsNullOrEmpty(credentials.Password))
            {
                Console.WriteLine("❌ Login ou Password vide");
                return BadRequest("Login et Password requis");
            }

            var token = _sessionService.Authenticate(credentials.Login, credentials.Password);

            if (token == null)
            {
                Console.WriteLine("❌ AUTHENTIFICATION ÉCHOUÉE");
                return Unauthorized(new { message = "Identifiants incorrects" });
            }

            Console.WriteLine($"✅ AUTHENTIFICATION RÉUSSIE - Token: {token.Value}");
            return Ok(token);
        }
    }
}


