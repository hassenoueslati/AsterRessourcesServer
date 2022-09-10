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
using BackOfficeAPI.Services.EmailService;
using Microsoft.AspNetCore.WebUtilities;
using Google.Apis.Auth;
using BackOfficeAPI.Data.TokenConfig;
using System.Text.Json;

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
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;

        public UserController(Context context, UserManager<User> userManager, 
                              RoleManager<IdentityRole> roleManager,IConfiguration configuration, 
                              IEmailService emailService,
                              ITokenHandler tokenHandler,
                              IHttpClientFactory httpClientFactory,
                              IWebHostEnvironment env
                              )
        {
            this._context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();
            _env = env;
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
            var users = _context.Users.ToList();
            foreach (var user in users)
            {
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
                if (user.Role == Role.Candidat)
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

        // GET: api/User/GetUser/Email@gmail.com
        [HttpGet("GetUserByEmail/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {

            List<User> AllUser = new List<User>();
            var users = _context.Users.ToList();

            foreach (var user in users)
            {
                if (user.Email == email)
                {
                    AllUser.Add(user);
                }
            }
            var User = AllUser[0];

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

        // PUT: api/User/UpdateUser/Email@gmail.com
        [HttpPut("UpdateUserByEmail/{email}")]
        public async Task<IActionResult> UpdateUserByEmail(string email, User User)
        {
            if (email != User.Email)
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
                if (!UserExists(email))
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

        private bool UserExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
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
                user.Statut = true;
                var userDetail = new RegisterModel { Nom = user.Nom, Prenom = user.Prenom, Email = user.Email, Role = user.Role.ToString() };
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Status = new Response().Status = "Success",
                    DataSet = new Response().DataSet = userDetail
                }
                    );
            }
            return Unauthorized(
               new Response { Status = "Error", Message = "Invalid Email or Password" });

        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginModel model)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["Google:ClientId"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
            var candidatExists = await userManager.FindByNameAsync(payload.Email);
                if (candidatExists == null)
                {
                    Candidat candidat = new()
                    {
                        Email = payload.Email,
                        UserName = payload.Email,
                        Nom = model.FirstName,
                        Prenom = model.LastName,
                        Role = Role.Candidat,
                        Image = model.PhotoUrl

                    };
                    var result = await userManager.CreateAsync(candidat);
                    Token token = _tokenHandler.CreateAccessToken(5, candidat);
                    var userDetail = new RegisterModel { Nom = candidat.Nom, Prenom = candidat.Prenom, Email = candidat.Email, Role = candidat.Role.ToString() };
                    return Ok(new
                        {
                            token = token.AccessToken,
                            expiration = token.Expiration,
                            Status = new Response().Status = "Success",
                            Message = new Response().Message = "User registered in the website and connected with Google successfully !",
                            DataSet = new Response().DataSet = userDetail
                        });
                }        
                else
                {
                    Token token = _tokenHandler.CreateAccessToken(5, candidatExists);

                    var userDetail = new RegisterModel { Nom = candidatExists.Nom, Prenom = candidatExists.Prenom, Email = candidatExists.Email, Role = candidatExists.Role.ToString() };
                    return Ok(new
                    {
                        token = token.AccessToken,
                        expiration = token.Expiration,
                        Status = new Response().Status = "Success",
                        Message = new Response().Message = "User Connected with Google successfully !",
                        DataSet = new Response().DataSet = userDetail
                    }
                    );

                }           
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginModel model)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_configuration["Facebook:ClientId"]}&client_secret={_configuration["Facebook:ClientSecret"]}&grant_type=client_credentials");
            
            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AuthToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookUserAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,first_name,last_name&access_token={model.AuthToken}");
                
                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var candidatExists = await userManager.FindByNameAsync(userInfo.Email);
                if (candidatExists == null)
                {
                    Candidat candidat = new()
                    {
                        Email = userInfo.Email,
                        UserName = userInfo.Email,
                        Nom = userInfo.FirstName,
                        Prenom = userInfo.LastName,
                        Role = Role.Candidat,
                        Image = model.PhotoUrl

                    };
                    var result = await userManager.CreateAsync(candidat);
                    Token token = _tokenHandler.CreateAccessToken(5, candidat);
                    var userDetail = new RegisterModel { Nom = candidat.Nom, Prenom = candidat.Prenom, Email = candidat.Email, Role = candidat.Role.ToString() };
                    return Ok(new
                    {
                        token = token.AccessToken,
                        expiration = token.Expiration,
                        Status = new Response().Status = "Success",
                        Message = new Response().Message = "User registered in the website and connected with Facebook successfully !",
                        DataSet = new Response().DataSet = userDetail
                    });
                }
                else
                {
                    Token token = _tokenHandler.CreateAccessToken(5, candidatExists);

                    var userDetail = new RegisterModel { Nom = candidatExists.Nom, Prenom = candidatExists.Prenom, Email = candidatExists.Email, Role = candidatExists.Role.ToString() };
                    return Ok(new
                    {
                        token = token.AccessToken,
                        expiration = token.Expiration,
                        Status = new Response().Status = "Success",
                        Message = new Response().Message = "User Connected with Facebook successfully !",
                        DataSet = new Response().DataSet = userDetail
                    }
                    );

                }
            }

            return Unauthorized();
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

            return Ok(new Response { Status = "Success", Message = "User created successfully!", DataSet = userDetail });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Admin admin = new Admin()
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Nom = model.Nom,
                Prenom = model.Prenom,
                Role = Role.Admin
            };
            var result = await userManager.CreateAsync(admin, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userDetail = new RegisterModel { Nom = admin.Nom, Prenom = admin.Prenom, Email = admin.Email, Role = admin.Role.ToString() };

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
                Role = Role.Candidat,
                Telephone = model.Telephone,
                Image = model.Image

            };
            var result = await userManager.CreateAsync(candidat, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            var userDetail = new RegisterCandidatModel { Nom = candidat.Nom, Prenom = candidat.Prenom, Email = candidat.Email, Role = candidat.Role.ToString(), Telephone = candidat.Telephone };

            return Ok(new Response { Status = "Success", Message = "User created successfully!", DataSet = userDetail });
        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "User does not exists!" });

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = "the new Password and confirm new password does not match !" });

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                var errors = new List<string>();

                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = string.Join(", ", errors) });


            }
            return Ok(new Response { Status = "Success", Message = "Password successfully changed." });
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(EmailModel model)
        {
            await _emailService.SendEmail(model);
            return Ok(new Response { Status = "Success", Message = "Mail sent successfully ." });
        }

        [HttpPost]
        [Route("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return NotFound();
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response
                {
                    Status = "Error",
                    Message = "No user associated with email!"
                });
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppAdminUrl"]}/resetPassword/{model.Email}/{validToken}";

            var EmailSanded = new EmailModel
            {
                To = model.Email,
                Subject = "Réinitialisation de mot de passe",
                Body = $"<h3>Bonjour monsieur {user.Nom} {user.Prenom}, </h3>" +
                    "<br>" +
                    $"<h4>Une demande de réinitialisation de mot de passe a été demandée pour votre compte utilisateur {user.Email}, </h4>" +
                    $"<h4>Pour confirmer cette demande et définir un nouveau mot de passe, veuillez <a href='{url}'>cliquer ci</a>.</h4>" +
                    "<h4>Si vous avez besoin d'aide, veuillez contacter l'administrateur du site.</h4>" +
                    "<br>"+
                    "<h4>Admin</h4>"+
                    "<h4>info@aster-ressources.ca</h4>"
            };
         
            await _emailService.SendEmail(EmailSanded);

            return Ok(new Response
            {
                Status = "Success",
                Message = "Reset password URL has been sent to the email successfully!"
            });
        }


        [HttpPost]
        [Route("forget-password-candidat")]
        public async Task<IActionResult> ForgetPasswordCandidat(ForgetPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return NotFound();
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response
                {
                    Status = "Error",
                    Message = "No candidat associated with email!"
                });
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string url = $"{_configuration["AppUrl"]}/resetPassword/{model.Email}/{validToken}";

            var EmailSanded = new EmailModel
            {
                To = model.Email,
                Subject = "Réinitialisation de mot de passe",
                Body = $"<h3>Bonjour monsieur {user.Nom} {user.Prenom}, </h3>" +
                    "<br>" +
                    $"<h4>Une demande de réinitialisation de mot de passe a été demandée pour votre compte utilisateur {user.Email}, </h4>" +
                    $"<h4>Pour confirmer cette demande et définir un nouveau mot de passe, veuillez <a href='{url}'>cliquer ci</a>.</h4>" +
                    "<h4>Si vous avez besoin d'aide, veuillez contacter l'administrateur du site.</h4>" +
                    "<br>" +
                    "<h4>Admin</h4>" +
                    "<h4>info@aster-ressources.ca</h4>"
            };

            await _emailService.SendEmail(EmailSanded);

            return Ok(new Response
            {
                Status = "Success",
                Message = "Reset password URL has been sent to the email successfully!"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.Email))
                return NotFound();
            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response
                {
                    Status = "Error",
                    Message = "No user associated with email!"
                });
            if (model.NewPassword != model.ConfirmPassword)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { 
                        Status = "Error", 
                        Message = "Password doesn't match its confirmation" 
                    });

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);
            
            if (result.Succeeded)
                return Ok(new Response
                {
                    Status = "Success",
                    Message = "Password has been reset successfully!"
                });
           
            return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "Something went wrong",
                    });
        }

        [Route("SaveImage")]
        [HttpPost]
        public JsonResult SaveImage()
        {
            try
            {
                int Max;
                Max = IDMax() + 1;
                var httpRequest = Request.Form;
                var postFile = httpRequest.Files[0];
                string filename = postFile.FileName;
                var physicalPath = _env.ContentRootPath+ "/Images/" + Max + filename ;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postFile.CopyTo(stream);
                }
                return new JsonResult(Max);

            }
            catch (Exception)
            {
                return new JsonResult("Invalid image");
            }
        }

        private int IDMax()
        {
            var ListUsers = _context.Users.AsQueryable();
            var User = ListUsers.OrderByDescending(x => x.UserId).FirstOrDefault();
            var maxID = User != null ? User.UserId : 0;
            return maxID;
        }
    }
}
