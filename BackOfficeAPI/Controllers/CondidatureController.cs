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
            this._context = context;
        }

        // GET: api/Condidatures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Condidature>>> GetCondidatures()
        {
            return await _context.Condidatures.ToListAsync();
        }

        // GET: api/Condidatures/5
        [HttpGet("{idOffre}/{idCandidat}")]
        public async Task<ActionResult<Condidature>> GetCondidature(int idOffre, int idCandidat)
        {
            var Condidature = await _context.Condidatures.FindAsync(idOffre, idCandidat);

            if (Condidature == null)
            {
                return NotFound();
            }

            return Condidature;
        }

        //// PUT: api/Condidatures/5
        //[HttpPut("{id1}/{id2}")]
        //public async Task<IActionResult> UpdateCondidature(int id1, int id2, Condidature Condidature)
        //{
        //    if ((id1 != Condidature.OffreFK) && (id2 != Condidature.CandidatFK))
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(Condidature).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CondidatureExists(id1, id2))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Condidatures
        [HttpPost]
        public async Task<ActionResult<Condidature>> AddCondidature(Condidature Condidature)
        {
            _context.Condidatures.Add(Condidature);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCondidature", 
                                    new { id = (Condidature.OffreFK, Condidature.CandidatFK) }
                                    , Condidature
                                    );
        }

        //// POST: api/Condidatures
        //[HttpPost]
        //public async Task<ActionResult<Candidat>> AddCondidature(Condidature Condidature)
        //{
        //    var candidat = await _context.Candidats.FindAsync(Condidature.CandidatFK);
        //    if(candidat == null)
        //        return NotFound();

        //    var offre = await _context.Offres.FindAsync(Condidature.OffreFK);
        //    if (offre == null)
        //        return NotFound();

        //    candidat.Condidatures.Add(Condidature);

        //    await _context.SaveChangesAsync();

        //    return candidat;


        //}

        // DELETE: api/Condidatures/5
        [HttpDelete("{idOffre}/{idCandidat}")]
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

        //private bool CondidatureExists(int id1, int id2)
        //{
        //    return _context.Condidatures.Any(e => e.OffreFK == id1 && e.CandidatFK == id2);
        //}
    }
}
