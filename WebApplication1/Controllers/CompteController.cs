using Microsoft.AspNetCore.Mvc;
using WebApplication1.Infrastructure.Database;
using WebApplication1.Models.Entites;
using System.Collections.Generic; // Ajouté pour List<T>
using System.Linq; // Ajouté pour Cast<T>

namespace WebApplication1.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CompteController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var comptes = Repo.DBRepo.TryGetValue(typeof(Compte), out var obj) && obj is List<Compte> list
                ? list
                : new List<Compte>();
            return Ok(comptes);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Compte compte)
        {
            var comptes = Repo.DBRepo.TryGetValue(typeof(Compte), out var obj) && obj is List<Compte> list
                ? list
                : new List<Compte>();
            comptes.Add(compte);
            Repo.DBRepo[typeof(Compte)] = comptes;
            return Ok(compte);
        }
    }
}
