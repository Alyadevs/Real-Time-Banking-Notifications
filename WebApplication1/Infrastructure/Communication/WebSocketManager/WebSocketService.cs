using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager;

namespace WebApplication1.Infrastructure.Communication.WebSocketManager
{
    public class WebSocketService
    {
        private readonly ISessionService _sessionService;

        public WebSocketService(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public bool TryAddSocket(string? token, WebSocket webSocket)
        {
            var session = _sessionService.GetSession(token);
            if (session != null)
            {
                session.WSocket = webSocket;
                Console.WriteLine($"[WebSocketService] Socket added to UserId={session.User.Id}");
                return true;
            }
            return false;
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            if (!int.TryParse(userId, out int uid)) return;

            var session = _sessionService.GetSessionByUserId(uid);
            if (session?.WSocket == null || session.WSocket.State != WebSocketState.Open) return;

            var notification = new
            {
                type = "NEGATIVE_BALANCE_ALERT",
                message,
                timestamp = DateTime.UtcNow,
                userId
            };

            var buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(notification));
            await session.WSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine($"✅ Notification sent to UserId={userId}");
            
        }
    }
}
