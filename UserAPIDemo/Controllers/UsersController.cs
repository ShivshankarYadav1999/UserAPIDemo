using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserAPIDemo.DTOs;
using UserAPIDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserAPIDemo.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyAppDbContext _myAppDbContext;

        public UsersController(MyAppDbContext context)
        {
            _myAppDbContext = context;
        }

        //Handle get request for all users
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _myAppDbContext.Users.ToList();
            return Ok(users);
        }

        //Handle get request to get single user by id
        [HttpGet("user/{Id}")]
        public async Task<ActionResult<Users>> GetUserById(int Id)
        {
            var user = await _myAppDbContext.Users.FindAsync(Id);

            if (user == null) { return NotFound(); }
            return user;
        }

        //Handle post request to add one user
        [HttpPost("user")]
        public async Task<ActionResult<Users>> Postuser(CreateUserDTO user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            var newUser = new Users
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedBy = user.CreatedBy,
                CreatedDate = DateTime.Now
            };
            _myAppDbContext.Users.Add(newUser);
            await _myAppDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        //Handle put request to update one user 
        [HttpPut("user/{id}")]
        public async Task<IActionResult> PutUserById(int id, UpdateUserDTO user)
        {
            var existingUser = await _myAppDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.ModifiedDate = DateTime.Now;
            if(user.ModifiedUserId != null)
                existingUser.ModifiedUserId = user.ModifiedUserId;
            await _myAppDbContext.SaveChangesAsync();
            return NoContent();
        }

        //Handle patch request to update one user
        [HttpPatch("user/{id}")]
        public async Task<IActionResult> UpdateUserById(int id, UpdateUserDTO user)
        {
            var existingUser = await _myAppDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            if(!string.IsNullOrEmpty(user.FirstName))
                existingUser.FirstName = user.FirstName;
            
            if (!string.IsNullOrEmpty(user.LastName))
                existingUser.LastName = user.LastName;
            
            existingUser.ModifiedDate = DateTime.Now;
      
            if(user.ModifiedUserId != null)
                existingUser.ModifiedUserId = user.ModifiedUserId;

            await _myAppDbContext.SaveChangesAsync();
            return NoContent();
        }

        //Handle delete request to delete one user
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            var user = await _myAppDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _myAppDbContext.Users.Remove(user);
            await _myAppDbContext.SaveChangesAsync();
            return NoContent();
        }

    }
}
