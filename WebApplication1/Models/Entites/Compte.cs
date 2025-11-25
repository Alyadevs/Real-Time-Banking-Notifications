namespace WebApplication1.Models.Entites
{
    public enum TypeCompte { Débitant, Créditant }

    public class Compte
    {
        public string NumeroCompte { get; set; }
        public decimal Solde { get; set; }
        public TypeCompte Type { get; set; }
        public int UserId { get; set; }
    }

}
