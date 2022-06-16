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

        // GET: api/Offres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offre>>> GetOffres()
        {
            return await _context.Offres.ToListAsync();
        }

        // GET: api/Offres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Offre>> GetOffre(int id)
        {
            var Offre = await _context.Offres.FindAsync(id);

            if (Offre == null)
            {
                return NotFound();
            }

            return Offre;
        }

        // PUT: api/Offres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
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

        // POST: api/Offres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Offre>> AddOffre(Offre Offre)
        {
            _context.Offres.Add(Offre);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffre", new { id = Offre.OffreId }, Offre);
        }

        // DELETE: api/Offres/5
        [HttpDelete("{id}")]
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
