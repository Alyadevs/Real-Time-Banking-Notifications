using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using WebApplication1.Infrastructure.Communication.WebSocketManager;

namespace WebApplication1.Middleware.CustomMiddleware
{
    public class CustomWebSocket
    {
        private readonly RequestDelegate _next;
        private readonly WebSocketService _webSocketService;

        public CustomWebSocket(RequestDelegate next, WebSocketService webSocketService)
        {
            _next = next;
            _webSocketService = webSocketService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket ws = await context.WebSockets.AcceptWebSocketAsync();
                var token = context.Request.Query["token"].ToString();

                _webSocketService.TryAddSocket(token, ws);
                await Receive(ws);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task Receive(WebSocket ws)
        {
            var buffer = new byte[1024 * 4];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fermeture", CancellationToken.None);
            }
        }
    }

    public static class WebSocketServerMiddleWareExtention
    {
        public static IApplicationBuilder UseWebSocketMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomWebSocket>();
        }
    }
}
