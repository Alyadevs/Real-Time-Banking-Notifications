using WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager;
using WebApplication1.Infrastructure.Communication.WebSocketManager;
using WebApplication1.Middleware.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Services pour WebSocket et Session
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddSingleton<WebSocketService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Configuration WebSocket
app.UseWebSockets();

// ✅ Middleware personnalisés
app.UseCustomAuthentification();
app.UseWebSocketMiddleWare();
app.MapControllers();
app.UseRouting();
app.UseAuthorization();

app.UseStaticFiles();

app.Run();
