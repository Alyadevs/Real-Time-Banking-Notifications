using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Infrastructure.Database;
using WebApplication1.Models.Entites;

namespace WebApplication1.Infrastructure.Communication.AccessAutorization.SessionManager
{
    public class SessionService : ISessionService
    {
        private readonly List<Session> _sessions = new();

        public Token? Authenticate(string login, string pwd)
        {
            var users = Repo.DBRepo[typeof(User)] as IEnumerable<User>;
            var user = users?.FirstOrDefault(u => u.Login == login && u.Password == pwd);
            return user != null ? NewSession(user) : null;
        }

        public Token NewSession(User user)
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.Now.AddMinutes(30)
            };

            _sessions.Add(new Session
            {
                Token = token,
                User = user
            });

            return token;
        }

        public Session? GetSession(string? token)
        {
            return token == null ? null : _sessions.FirstOrDefault(s => s.Token.Value == token && s.Token.ExpiryDate > DateTime.Now);
        }

        public Session? GetSessionByUserId(int userId)
        {
            return _sessions.FirstOrDefault(s => s.User.Id == userId && s.Token.ExpiryDate > DateTime.Now);
        }

        public IEnumerable<Session> GetAllSessions() => _sessions;
    }
}
