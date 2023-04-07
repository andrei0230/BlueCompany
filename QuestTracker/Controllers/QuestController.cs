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

        [HttpGet]
        public async Task<IEnumerable<Quest>> GetAll()
        {
            return await _context.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Quest> GetById(int id)
        {
            return await _context.GetById(id);
        }

        [HttpPost]
        public async Task<int> Create(Quest quest)
        {
            return await _context.Create(quest);
        }

        [HttpPut("{id}")]
        public async Task<int> Update(int id, Quest quest)
        {
            quest.Id = id;
            return await _context.Update(quest);
        }
    }
}
