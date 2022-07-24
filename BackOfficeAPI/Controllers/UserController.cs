using BackOfficeAPI.Data;
using BackOfficeAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

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


        // GET: api/User/GetAllUser
        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllUser")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllAdmin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAdmins()
        {
            List<User> AllAdmin = new List<User>();
            var users =  _context.Users.ToList();
            foreach (var user in users) {
                if (user.Role == Role.Admin)
                {
                    AllAdmin.Add(user);
                }
            }

            return AllAdmin;
        }

        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet("GetAllCandidat")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllCandidat()
        {
            List<User> AllCandidat = new List<User>();
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
                if (user.Role == Role.Condidat)
                {
                    AllCandidat.Add(user);
                }
            }

            return AllCandidat;
        }

        // GET: api/User/GetUser/5
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var User = await _context.Users.FindAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        // PUT: api/User/UpdateUser/5
        [HttpPut("UpdateUser/{id}")]
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

        // DELETE: api/User/DeleteUser/5
        [HttpDelete("DeleteUser/{id}")]
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
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role,user.Role.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(10),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                var userDetail = new RegisterModel { Nom = user.Nom, Prenom = user.Prenom, Email = user.Email, Role= user.Role.ToString() };
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Status = new Response().Status = "Success",
                    Message = new Response().Message = "",
                    DataSet = new Response().DataSet = userDetail
                }
                    ) ;
            }
            //return StatusCode(StatusCodes.Status500InternalServerError, 
            //    new Response { Status = "Error", Message = "Invalid Email or Password" });
            return Unauthorized(
               new Response { Status = "Error", Message = "Invalid Email or Password" });

        }
        
        [HttpPost]
        [Route("register-super-admin")]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Nom = model.Nom,
                Prenom = model.Prenom,
                Role = Role.SuperAdmin
                
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userDetail = new RegisterModel { Nom = user.Nom, Prenom = user.Prenom, Email = user.Email, Role = user.Role.ToString() };

            return Ok(new Response { Status = "Success", Message = "User created successfully!",DataSet= userDetail });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Nom = model.Nom,
                Prenom = model.Prenom,
                Role = Role.Admin

            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userDetail = new RegisterModel { Nom = user.Nom, Prenom = user.Prenom, Email = user.Email, Role = user.Role.ToString() };

            return Ok(new Response { Status = "Success", Message = "User created successfully!", DataSet = userDetail });
        }

        [HttpPost]
        [Route("register-candidat")]
        public async Task<IActionResult> RegisterCandidat([FromBody] RegisterCandidatModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Candidat candidat = new Candidat()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Nom = model.Nom,
                Prenom = model.Prenom,
                Role = Role.Condidat,
                Telephone = model.Telephone

            };
            var result = await userManager.CreateAsync(candidat, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userDetail = new RegisterCandidatModel { Nom = candidat.Nom, Prenom = candidat.Prenom, Email = candidat.Email, Role = candidat.Role.ToString(), Telephone = candidat.Telephone };

            return Ok(new Response { Status = "Success", Message = "User created successfully!", DataSet = userDetail });
        }
    }

   
}
