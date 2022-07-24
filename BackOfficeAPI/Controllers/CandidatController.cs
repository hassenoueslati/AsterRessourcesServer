using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatController : ControllerBase
    {
        private readonly Context _context;

        public CandidatController(Context context)
        {
            this._context = context;
        }

        // GET: api/Candidats
        [HttpGet("GetAllCandidat")]
        public async Task<ActionResult<IEnumerable<Candidat>>> GetCandidats()
        {
            return await _context.Candidats.ToListAsync();
        }

        // GET: api/Candidats/5
        [HttpGet("GetCandidat/{id}")]
        public async Task<ActionResult<Candidat>> GetCandidat(int id)
        {
            var Candidat = await _context.Candidats.FindAsync(id);

            if (Candidat == null)
            {
                return NotFound();
            }

            return Candidat;
        }

        // PUT: api/Candidats/5
        [HttpPut("UpdateCandidat/{id}")]
        public async Task<IActionResult> UpdateCandidat(int id, Candidat Candidat)
        {
            if (id != Candidat.UserId)
            {
                return BadRequest();
            }

            _context.Entry(Candidat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidatExists(id))
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

        // POST: api/Candidats
        [HttpPost("AddCandidat")]
        public async Task<ActionResult<Candidat>> AddCandidat(Candidat Candidat)
        {
            _context.Candidats.Add(Candidat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidat", new { id = Candidat.UserId }, Candidat);
        }

        // DELETE: api/Candidats/5
        [HttpDelete("DeleteCandidat/{id}")]
        public async Task<IActionResult> DeleteCandidat(int id)
        {
            var Candidat = await _context.Candidats.FindAsync(id);
            if (Candidat == null)
            {
                return NotFound();
            }

            _context.Candidats.Remove(Candidat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CandidatExists(int id)
        {
            return _context.Candidats.Any(e => e.UserId == id);
        }
    }
}
