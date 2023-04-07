using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTracker.Models;

namespace QuestTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly QuestContext _context;

        public QuestController(IConfiguration configuration)
        {
            _context = new QuestContext(configuration.GetConnectionString("DefaultConnection"));
        }

        [HttpGet("getall")]
        public async Task<IEnumerable<Quest>> GetAll()
        {
            return await _context.GetAll();
        }

        [HttpGet("getby/{id}")]
        public async Task<Quest> GetById(int id)
        {
            return await _context.GetById(id);
        }

        [HttpPost("create")]
        public async Task<int> Create(Quest quest)
        {
            return await _context.Create(quest);
        }

        [HttpPut("update/{id}")]
        public async Task<int> Update(int id, Quest quest)
        {
            quest.Id = id;
            return await _context.Update(quest);
        }

        [HttpDelete("delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _context.Delete(id);
        }

        [HttpPut("{id}/assign/{userId}")]
        public async Task<int> AssignToUser(int id, int userId)
        {
            return await _context.AssignToUser(id, userId);
        }

        [HttpPut("complete/{id}")]
        public async Task<int> Complete(int id)
        {
            return await _context.Complete(id);
        }
    }
}
