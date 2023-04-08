using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTracker.Data;
using QuestTracker.Models;

namespace QuestTracker.Controllers
{
    /// <summary>
    /// Contains Http requests for users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        /// <summary>
        /// Creates a new UserController.
        /// </summary>
        public UserController(IConfiguration configuration)
        {
            _context = new UserContext(configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        [HttpGet("getall")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.GetAll();
        }

        /// <summary>
        /// Gets the specified user.
        /// </summary>
        [HttpGet("getby/{id}")]
        public async Task<User> GetById(int id)
        {
            return await _context.GetById(id);
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <remarks>
        /// Request body example:
        ///
        ///     {   
        ///        "name": "Andrew"
        ///     }
        ///
        /// </remarks>
        [HttpPost("create")]
        public async Task<int> Create(User user)
        {
            return await _context.Create(user);
        }

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <remarks>
        /// Request body example:
        ///
        ///     {   
        ///        "name": "John"
        ///     }
        ///
        /// </remarks>
        [HttpPut("update/{id}")]
        public async Task<int> Update(int id, User user)
        {
            user.Id = id;
            return await _context.Update(user);
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _context.Delete(id);
        }
    }
}
