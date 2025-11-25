using WebApplication1.Models.Entites;

namespace WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager
{
    public interface ISessionService
    {
        public Token? Authenticate(string login, string pwd);
        public Token NewSession(User user);
        public Session? GetSession(string? token);
        Session? GetSessionByUserId(int userId);
        IEnumerable<Session> GetAllSessions();
    }
}