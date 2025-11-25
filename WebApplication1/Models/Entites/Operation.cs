namespace WebApplication1.Models.Entites
{
    public enum TypeOperation { Débit, Crédit }

    public class Operation
    {
        public int Id { get; set; }
        public DateTime DateHeure { get; set; }
        public TypeOperation Type { get; set; }
        public decimal Montant { get; set; }
        public string NumeroCompte { get; set; }
    }

}
