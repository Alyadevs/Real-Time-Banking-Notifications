using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure.Database;
using WebApplication1.Infrastructure.Communication.WebSocketManager;
using WebApplication1.Models.Entites;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly WebSocketService _webSocketService;

        public OperationController(WebSocketService webSocketService)
        {
            _webSocketService = webSocketService;
        }

        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] Operation op)
        {
            var comptes = Repo.DBRepo[typeof(Compte)] as List<Compte>;
            var compte = comptes?.FirstOrDefault(c => c.NumeroCompte == op.NumeroCompte);
            if (compte == null) return NotFound("Compte Not Found");

            decimal ancienSolde = compte.Solde;

            // Appliquer l'opération
            compte.Solde += op.Type == TypeOperation.Crédit ? op.Montant : -op.Montant;

            // Ajouter opération
            var operations = Repo.DBRepo[typeof(Operation)] as List<Operation>;
            op.Id = operations?.Count + 1 ?? 1;
            op.DateHeure = DateTime.Now;
            operations?.Add(op);

            // Alerte solde négatif
            if (compte.Solde < 0)
            {
                var message = $"⚠️ WARNING! Your account {compte.NumeroCompte} has a negative balance.\nNew balance: {compte.Solde:C}\nTransaction: {op.Type} of {op.Montant:C}";

                await _webSocketService.SendMessageToUser(compte.UserId.ToString(), message);
            }

            return Ok(new { operation = op, solde = compte.Solde });
        }
    }
}
