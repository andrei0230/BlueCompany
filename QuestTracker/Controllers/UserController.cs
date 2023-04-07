using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTracker.Models;

namespace QuestTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(IConfiguration configuration)
        {
            _context = new UserContext(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<User> GetById(int id)
        {
            return await _context.GetById(id);
        }

        [HttpPost]
        public async Task<int> Create(User user)
        {
            return await _context.Create(user);
        }

        [HttpPut("{id}")]
        public async Task<int> Update(int id, User user)
        {
            user.Id = id;
            return await _context.Update(user);
        }
    }
}
