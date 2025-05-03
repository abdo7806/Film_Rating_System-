using Film_Rating_System.Models;
using Film_Rating_System.Models.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Film_Rating_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IRepository<User> _UserRepository;
        private readonly AppDbContext _context;

        public UsersController(IRepository<User> UserRepository, AppDbContext context)
        {
            this._UserRepository = UserRepository;
            _context = context;
        }
        [HttpGet("{id}", Name = "GetUserById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<User> GetUserById(int id)
        {

            if (id < 1)
            {
                return BadRequest($"Not accepted ID {id}");
            }

            var user = _UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        // GET: api/<UsersController>
        [HttpGet("All", Name = "GetAllUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<User>> GetAllUsers() // Define a method to get all users.
        {

            var data = _UserRepository.GetAll();

            if (data.Count == 0)
            {
                return NotFound("No Users Found!");
            }
            return Ok(data); // Returns the list of users.
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Username and password are required.");

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            var claims = new[]
            {

        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("UserId", user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role ?? "User") // 👈 تضمين الدور في التوكن
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b7f9c5d6e1a4d2f3c7f8e6e2d5a1b5c7f9e4d2a7f8e6f3c2f1e7a6b8f9c5d4"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = token.ValidTo,
                Username = user.Username,
                Role = user.Role // 👈 عرض الدور في الاستجابة
            });
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
                return BadRequest("Username already exists.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                Role = string.IsNullOrEmpty(request.Role) ? "User" : request.Role // 👈 تحديد الدور أو استخدام "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully.",
                user = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Role, // 👈 عرض الدور
                    user.CreatedAt
                }
            });
        }


        // PUT api/<UsersController>/5
        [HttpPut("{id}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> UpdateUser(int id, [FromBody] RegisterRequest updatedUser)
        {
            if (updatedUser == null || string.IsNullOrEmpty(updatedUser.Username))
                
            {
                return BadRequest("Invalid user data.");
            }

            var user = _UserRepository.GetById(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            user.Username = updatedUser.Username;
           // user.PasswordHash = updatedUser.PasswordHash;
            user.Email = updatedUser.Email;
            user.CreatedAt = DateTime.Now;


            _UserRepository.Edit(id, user);

            return Ok(user);




        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int id)
        {


            int userID = _UserRepository.Delete(id);
            if (userID == -1)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok($"User with ID {id} has been deleted.");
        }

   
    }
}
