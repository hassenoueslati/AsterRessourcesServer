using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly Context _context;

        public ContactController(Context context)
        {
            this._context = context;
        }

        // GET: api/Contact/GetAllContact
        [HttpGet("GetAllContact")]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contact/GetContact/5
        [HttpGet("GetContact/{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var Contact = await _context.Contacts.FindAsync(id);

            if (Contact == null)
            {
                return NotFound();
            }

            return Contact;
        }

        // PUT: api/Contact/UpdateContact/5
        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact Contact)
        {
            if (id != Contact.ContactId)
            {
                return BadRequest();
            }

            _context.Entry(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contact/AddContact
        [HttpPost("AddContact")]
        public async Task<ActionResult<Contact>> AddContact(Contact Contact)
        {
            _context.Contacts.Add(Contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = Contact.ContactId }, Contact);
        }

        // DELETE: api/Contact/DeleteContact/5
        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var Contact = await _context.Contacts.FindAsync(id);
            if (Contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(Contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactId == id);
        }
    }
}
