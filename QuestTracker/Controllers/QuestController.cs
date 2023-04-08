using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestTracker.Data;
using QuestTracker.Models;

namespace QuestTracker.Controllers
{
    /// <summary>
    /// Contains Http requests for Quests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private readonly QuestContext _context;

        /// <summary>
        /// Creates a new QuestController.
        /// </summary>
        public QuestController(IConfiguration configuration)
        {
            _context = new QuestContext(configuration.GetConnectionString("DefaultConnection"));
        }
        
        /// <summary>
        /// Gets all Quests.
        /// </summary>
        [HttpGet("getall")]
        public async Task<IEnumerable<Quest>> GetAll()
        {
            return await _context.GetAll();
        }

        /// <summary>
        /// Gets the specified Quest.
        /// </summary>
        [HttpGet("getby/{id}")]
        public async Task<Quest> GetById(int id)
        {
            return await _context.GetById(id);
        }

        /// <summary>
        /// Creates a new Quest and subtracts the value from the owners tokens.
        /// </summary>
        /// <remarks>
        /// Request body example:
        ///
        ///     {   
        ///        "ownerId": 1,
        ///        "title": "Task 1",
        ///        "description": "Do something.",
        ///        "value": 10
        ///     }
        ///
        /// </remarks>
        [HttpPost("create")]
        public async Task<int> Create(Quest quest)
        {
            return await _context.Create(quest);
        }

        /// <summary>
        /// Updates the specified Quests title and description.
        /// </summary>
        /// <remarks>
        /// Request body example:
        ///
        ///     {   
        ///        "title": " New Task 1",
        ///        "description": "Do something else.",
        ///     }
        ///
        /// </remarks>
        [HttpPut("update/{id}")]
        public async Task<int> Update(int id, Quest quest)
        {
            quest.Id = id;
            return await _context.Update(quest);
        }

        /// <summary>
        /// Deletes the specified Quest.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _context.Delete(id);
        }

        /// <summary>
        /// Assigns a Quest to a user.
        /// </summary>
        [HttpPut("{id}/assign/{userId}")]
        public async Task<int> AssignToUser(int id, int userId)
        {
            return await _context.AssignToUser(id, userId);
        }

        /// <summary>
        /// Completes the specified Quest and adds tokens to the assigned user.
        /// </summary>
        [HttpPut("complete/{id}")]
        public async Task<int> Complete(int id)
        {
            return await _context.Complete(id);
        }
    }
}
