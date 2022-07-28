using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatureController : ControllerBase
    {
        private readonly Context _context;

        public CandidatureController(Context context)
        {
            _context = context;

        }

        // GET: api/Candidature/GetAllCandidature
        [HttpGet("GetAllCandidature")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetCandidatures()
        {
            return await _context.Candidatures.ToListAsync();
        }

        // GET: api/Candidature/GetAllCandidatureActif
        [HttpGet("GetAllCandidatureActif/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureActif(int idOffre)
        {
            List<Candidature> AllCandidatureActif = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.Actif && candidature.OffreFK == idOffre)
                {
                    AllCandidatureActif.Add(candidature);
                }
            }

            return AllCandidatureActif;
        }

        // GET: api/Candidature/GetAllCandidatureAExaminer
        [HttpGet("GetAllCandidatureAExaminer/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureAExaminer(int idOffre)
        {
            List<Candidature> AllCandidatureAExaminer = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.AExaminer && candidature.OffreFK == idOffre)
                {
                    AllCandidatureAExaminer.Add(candidature);
                }
            }

            return AllCandidatureAExaminer;
        }

        // GET: api/Candidature/GetAllCandidatureExaminer
        [HttpGet("GetAllCandidatureExaminer/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureExaminer(int idOffre)
        {
            List<Candidature> AllCandidatureExaminer = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.Examine && candidature.OffreFK == idOffre)
                {
                    AllCandidatureExaminer.Add(candidature);
                }
            }

            return AllCandidatureExaminer;
        }

        // GET: api/Candidature/GetAllCandidatureEnCommunication
        [HttpGet("GetAllCandidatureEnCommunication/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureEnCommunication(int idOffre)
        {
            List<Candidature> AllCandidatureEnCommunication = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.EnCommunication && candidature.OffreFK == idOffre)
                {
                    AllCandidatureEnCommunication.Add(candidature);
                }
            }

            return AllCandidatureEnCommunication;
        }

        // GET: api/Candidature/GetAllCandidatureRecruter
        [HttpGet("GetAllCandidatureRecruter/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureRecruter(int idOffre)
        {
            List<Candidature> AllCandidatureRecruter = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.Recrute && candidature.OffreFK == idOffre)
                {
                    AllCandidatureRecruter.Add(candidature);
                }
            }

            return AllCandidatureRecruter;
        }

        // GET: api/Candidature/GetAllCandidatureNonRetenu
        [HttpGet("GetAllCandidatureNonRetenu/{idOffre}")]
        public async Task<ActionResult<IEnumerable<Candidature>>> GetAllCandidatureNonRetenu(int idOffre)
        {
            List<Candidature> AllCandidatureNonRetenu = new List<Candidature>();
            var candidatures = _context.Candidatures.ToList();
            foreach (var candidature in candidatures)
            {
                if (candidature.Etat == EtatCandidature.NonRetenu && candidature.OffreFK == idOffre)
                {
                    AllCandidatureNonRetenu.Add(candidature);
                }
            }

            return AllCandidatureNonRetenu;
        }


        // GET: api/Candidature/GetCandidature/5/2
        [HttpGet("GetCandidature/{idOffre}/{idCandidat}")]
        public async Task<ActionResult<Candidature>> GetCandidature(int idOffre, int idCandidat)
        {
            var Candidature = await _context.Candidatures.FindAsync(idOffre, idCandidat);

            if (Candidature == null)
            {
                return NotFound();
            }

            return Candidature;
        }


        // PUT: api/Candidature/UpdateCandidature/5/5
        [HttpPut("UpdateCandidature/{idOffre}/{idCandidat}")]
        public async Task<IActionResult> UpdateCandidature(int idOffre, int idCandidat, Candidature Candidature)
        {
            if ((idOffre != Candidature.OffreFK) && (idCandidat != Candidature.CandidatFK))
            {
                return BadRequest();
            }

            _context.Entry(Candidature).State = EntityState.Modified;

           
                await _context.SaveChangesAsync();

            return new JsonResult("Candidature Updated successfully !");
        }

        // POST: api/Candidature/AddCandidature
        [HttpPost("AddCandidature")]
        public async Task<ActionResult<Candidature>> AddCandidature(Candidature Candidature)
        {
            _context.Candidatures.Add(Candidature);
            await _context.SaveChangesAsync();

            return new JsonResult("Candidature Added successfully !");
        }


        // DELETE: api/Candidature/DeleteCandidature/5
        [HttpDelete("DeleteCandidature/{idOffre}/{idCandidat}")]
        public async Task<IActionResult> DeleteCandidature(int idOffre, int idCandidat)
        {
            var Candidature = await _context.Candidatures.FindAsync(idOffre, idCandidat);
            if (Candidature == null)
            {
                return NotFound();
            }

            _context.Candidatures.Remove(Candidature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
