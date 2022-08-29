using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffreController : ControllerBase
    {

        private readonly Context _context;

        public OffreController(Context context)
        {
            this._context = context;
        }

        // GET: api/Offre/GetAllOffre
        [HttpGet("GetAllOffre")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetOffres()
        {
            return await _context.Offres.ToListAsync();
        }

        // GET: api/Offre/GetAllOffreOuverte
        [HttpGet("GetAllOffreOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreOuverte()
        {
            List<Offre> AllOffreOuverte = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Etat == EtatOffre.Ouverte)
                {
                    AllOffreOuverte.Add(offre);
                }
            }

            return AllOffreOuverte.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreSuspendu
        [HttpGet("GetAllOffreSuspendu")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreSuspendu()
        {
            List<Offre> AllOffreSuspendu = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Etat == EtatOffre.Suspendu)
                {
                    AllOffreSuspendu.Add(offre);
                }
            }

            return AllOffreSuspendu.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreFermer
        [HttpGet("GetAllOffreFermer")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreFermer()
        {
            List<Offre> AllOffreFermer = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Etat == EtatOffre.Fermé)
                {
                    AllOffreFermer.Add(offre);
                }
            }

            return AllOffreFermer.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreMenuisier
        [HttpGet("GetAllOffreMenuisier")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreMenuisier()
        {
            List<Offre> AllOffreMenuisier = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.Menuisier))
                {
                    AllOffreMenuisier.Add(offre);
                }
            }

            return AllOffreMenuisier.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreChefCuisine
        [HttpGet("GetAllOffreChefCuisine")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreChefCuisine()
        {
            List<Offre> AllOffreChefCuisine = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.ChefCuisine))
                {
                    AllOffreChefCuisine.Add(offre);
                }
            }

            return AllOffreChefCuisine.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreMachiniste
        [HttpGet("GetAllOffreMachiniste")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreMachiniste()
        {
            List<Offre> AllOffreMachiniste = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.Machiniste))
                {
                    AllOffreMachiniste.Add(offre);
                }
            }

            return AllOffreMachiniste.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreJournalierDeProduction
        [HttpGet("GetAllOffreJournalierDeProduction")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreJournalierDeProduction()
        {
            List<Offre> AllOffreJournalierDeProduction = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.JournalierDeProduction))
                {
                    AllOffreJournalierDeProduction.Add(offre);
                }
            }

            return AllOffreJournalierDeProduction.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreCommisEntrepot
        [HttpGet("GetAllOffreCommisEntrepot")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreCommisEntrepot()
        {
            List<Offre> AllOffreCommisEntrepot = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.CommisEntrepôt))
                {
                    AllOffreCommisEntrepot.Add(offre);
                }
            }

            return AllOffreCommisEntrepot.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreEtalagisteDecorateur
        [HttpGet("GetAllOffreEtalagisteDecorateur")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreEtalagisteDecorateur()
        {
            List<Offre> AllOffreEtalagisteDecorateur = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.EtalagisteDécorateur))
                {
                    AllOffreEtalagisteDecorateur.Add(offre);
                }
            }

            return AllOffreEtalagisteDecorateur.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreOuverteDate
        [HttpGet("GetAllOffreOuverteDate")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreOuverteDate()
        {
            List<Offre> AllOffreOuverte = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreOuverte.Add(offre);
                }
            }

            return AllOffreOuverte.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreMenuisierOuverte
        [HttpGet("GetAllOffreMenuisierOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreMenuisierOuverte()
        {
            List<Offre> AllOffreMenuisier = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.Menuisier) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now  >= offre.DateOuverture))
                {
                    AllOffreMenuisier.Add(offre);
                }
            }

            return AllOffreMenuisier.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreChefCuisineOuverte
        [HttpGet("GetAllOffreChefCuisineOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreChefCuisineOuverte()
        {
            List<Offre> AllOffreChefCuisine = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.ChefCuisine) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreChefCuisine.Add(offre);
                }
            }

            return AllOffreChefCuisine.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreMachinisteOuverte
        [HttpGet("GetAllOffreMachinisteOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreMachinisteOuverte()
        {
            List<Offre> AllOffreMachiniste = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.Machiniste) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreMachiniste.Add(offre);
                }
            }

            return AllOffreMachiniste.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreJournalierDeProductionOuverte
        [HttpGet("GetAllOffreJournalierDeProductionOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreJournalierDeProductionOuverte()
        {
            List<Offre> AllOffreJournalierDeProduction = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.JournalierDeProduction) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreJournalierDeProduction.Add(offre);
                }
            }

            return AllOffreJournalierDeProduction.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreCommisEntrepotOuverte
        [HttpGet("GetAllOffreCommisEntrepotOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreCommisEntrepotOuverte()
        {
            List<Offre> AllOffreCommisEntrepot = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.CommisEntrepôt) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreCommisEntrepot.Add(offre);
                }
            }

            return AllOffreCommisEntrepot.OrderByDescending(x => x.OffreId).ToList();
        }

        // GET: api/Offre/GetAllOffreEtalagisteDecorateurOuverte
        [HttpGet("GetAllOffreEtalagisteDecorateurOuverte")]
        public async Task<ActionResult<IEnumerable<Offre>>> GetAllOffreEtalagisteDecorateurOuverte()
        {
            List<Offre> AllOffreEtalagisteDecorateur = new List<Offre>();
            var offres = _context.Offres.ToList();
            foreach (var offre in offres)
            {
                if (offre.Proffesions.Contains(Proffesion.EtalagisteDécorateur) && offre.Etat == EtatOffre.Ouverte && (DateTime.Now >= offre.DateOuverture))
                {
                    AllOffreEtalagisteDecorateur.Add(offre);
                }
            }

            return AllOffreEtalagisteDecorateur.OrderByDescending(x => x.OffreId).ToList();
        }




        // GET: api/Offre/GetOffre/5
        [HttpGet("GetOffre/{id}")]
        public async Task<ActionResult<Offre>> GetOffre(int id)
        {
            var Offre = await _context.Offres.FindAsync(id);

            if (Offre == null)
            {
                return NotFound();
            }

            return Offre;
        }

        // PUT: api/Offre/UpdateOffre/5
        [HttpPut("UpdateOffre/{id}")]
        public async Task<IActionResult> UpdateOffre(int id, Offre Offre)
        {
            if (id != Offre.OffreId)
            {
                return BadRequest();
            }

            _context.Entry(Offre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Offre/AddOffre
        [HttpPost("AddOffre")]
        public async Task<ActionResult<Offre>> AddOffre(Offre Offre)
        {
            _context.Offres.Add(Offre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffre", new { id = Offre.OffreId }, Offre);
        }

        // DELETE: api/Offre/DeleteOffre/5
        [HttpDelete("DeleteOffre/{id}")]
        public async Task<IActionResult> DeleteOffre(int id)
        {
            var Offre = await _context.Offres.FindAsync(id);
            if (Offre == null)
            {
                return NotFound();
            }

            _context.Offres.Remove(Offre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OffreExists(int id)
        {
            return _context.Offres.Any(e => e.OffreId == id);
        }
    }
}
