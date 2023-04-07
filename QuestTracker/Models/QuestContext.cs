using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace QuestTracker.Models
{
    public class QuestContext
    {
        private readonly IDbConnection _db;

        public QuestContext(string? connectionString)
        {
            _db = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<Quest>> GetAll()
        {
            return await _db.QueryAsync<Quest>("SELECT * FROM quests");
        }

        public async Task<Quest> GetById(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<Quest>("SELECT * FROM quests WHERE id = @Id", new { Id = id });
        }

        public async Task<int> Create(Quest quest)
        {
            return await _db.ExecuteAsync("INSERT INTO quests (title, description, value) VALUE (@Title, @Description, @Value)", quest);
        }
    }
}
