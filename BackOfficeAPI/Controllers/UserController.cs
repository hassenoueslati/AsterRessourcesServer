using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Context _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public UserController(Context context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this._context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User User)
        {
            if (id != User.UserId)
            {
                return BadRequest();
            }

            _context.Entry(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User User)
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = User.UserId }, User);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var User = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }


        [HttpPost]
        [Route("register-super-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Nom = model.Nom,
                Prenom = model.Prenom
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
            {
                await userManager.AddToRoleAsync(user, UserRoles.SuperAdmin);
            }

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }
    }
}
