using WebApplication1.Models.Entites;

namespace WebApplication1.Infrastructure.Database
{
    public static class Repo
    {
        public static Dictionary<Type, object> DBRepo = new();


        static Repo()   
        {
            DBRepo[typeof(User)] = new List<User>
        {
            new User { Id = 1, Login = "1", Password = "1", FullName = "Hamza" },
            new User { Id = 2, Login = "2", Password = "2", FullName = "Abdou" }
        };

            DBRepo[typeof(Compte)] = new List<Compte>
        {
            new Compte { NumeroCompte = "C001", Solde = 100, Type = TypeCompte.Débitant, UserId = 1 },
            new Compte { NumeroCompte = "C002", Solde = 500, Type = TypeCompte.Créditant, UserId = 2 }
        };

            DBRepo[typeof(Operation)] = new List<Operation>();
        }
    }

}
