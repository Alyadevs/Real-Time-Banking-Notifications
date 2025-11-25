using System.Net.WebSockets;
using WebApplication1.Models.Entites;

namespace WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager
{
    public class Session
    {
        public Token Token { get; set; }
        public WebSocket WSocket { get; set; }
        public User User { get; set; }
    }

}
