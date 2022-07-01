using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondidatureController : ControllerBase
    {
        private readonly Context _context;

        public CondidatureController(Context context)
        {
            _context = context;

        }

        // GET: api/Condidature/GetAllCondidature
        [HttpGet("GetAllCondidature")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetCondidatures()
        {
            return await _context.Condidatures.ToListAsync();
        }

        // GET: api/Condidature/GetAllCondidatureActif
        [HttpGet("GetAllCondidatureActif")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureActif()
        {
            List<Condidature> AllCondidatureActif = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.Actif)
                {
                    AllCondidatureActif.Add(condidature);
                }
            }

            return AllCondidatureActif;
        }

        // GET: api/Condidature/GetAllCondidatureAExaminer
        [HttpGet("GetAllCondidatureAExaminer")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureAExaminer()
        {
            List<Condidature> AllCondidatureAExaminer = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.AExaminer)
                {
                    AllCondidatureAExaminer.Add(condidature);
                }
            }

            return AllCondidatureAExaminer;
        }

        // GET: api/Condidature/GetAllCondidatureExaminer
        [HttpGet("GetAllCondidatureExaminer")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureExaminer()
        {
            List<Condidature> AllCondidatureExaminer = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.Examine)
                {
                    AllCondidatureExaminer.Add(condidature);
                }
            }

            return AllCondidatureExaminer;
        }

        // GET: api/Condidature/GetAllCondidatureEnCommunication
        [HttpGet("GetAllCondidatureEnCommunication")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureEnCommunication()
        {
            List<Condidature> AllCondidatureEnCommunication = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.EnCommunication)
                {
                    AllCondidatureEnCommunication.Add(condidature);
                }
            }

            return AllCondidatureEnCommunication;
        }

        // GET: api/Condidature/GetAllCondidatureRecruter
        [HttpGet("GetAllCondidatureRecruter")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureRecruter()
        {
            List<Condidature> AllCondidatureRecruter = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.Recrute)
                {
                    AllCondidatureRecruter.Add(condidature);
                }
            }

            return AllCondidatureRecruter;
        }

        // GET: api/Condidature/GetAllCondidatureNonRetenu
        [HttpGet("GetAllCondidatureNonRetenu")]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetAllCondidatureNonRetenu()
        {
            List<Condidature> AllCondidatureNonRetenu = new List<Condidature>();
            var condidatures = _context.Condidatures.ToList();
            foreach (var condidature in condidatures)
            {
                if (condidature.Etat == EtatCondidature.NonRetenu)
                {
                    AllCondidatureNonRetenu.Add(condidature);
                }
            }

            return AllCondidatureNonRetenu;
        }


        // GET: api/Condidature/GetCondidature/5/2
        [HttpGet("GetCondidature/{idOffre}/{idCandidat}")]
        public async Task<ActionResult<Condidature>> GetCondidature(int idOffre, int idCandidat)
        {
            var Condidature = await _context.Condidatures.FindAsync(idOffre, idCandidat);

            if (Condidature == null)
            {
                return NotFound();
            }

            return Condidature;
        }


        // PUT: api/Condidature/UpdateCondidature/5/5
        [HttpPut("UpdateCondidature/{idOffre}/{idCandidat}")]
        public async Task<IActionResult> UpdateCondidature(int idOffre, int idCandidat, Condidature Condidature)
        {
            if ((idOffre != Condidature.OffreFK) && (idCandidat != Condidature.CandidatFK))
            {
                return BadRequest();
            }

            _context.Entry(Condidature).State = EntityState.Modified;

           
                await _context.SaveChangesAsync();

            return new JsonResult("Condidature Updated successfully !");
        }

        // POST: api/Condidature/AddCondidature
        [HttpPost("AddCondidature")]
        public async Task<ActionResult<Condidature>> AddCondidature(Condidature Condidature)
        {
            _context.Condidatures.Add(Condidature);
            await _context.SaveChangesAsync();

            return new JsonResult("Condidature Added successfully !");
        }


        // DELETE: api/Condidature/DeleteCondidature/5
        [HttpDelete("DeleteCondidature/{idOffre}/{idCandidat}")]
        public async Task<IActionResult> DeleteCondidature(int idOffre, int idCandidat)
        {
            var Condidature = await _context.Condidatures.FindAsync(idOffre, idCandidat);
            if (Condidature == null)
            {
                return NotFound();
            }

            _context.Condidatures.Remove(Condidature);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
