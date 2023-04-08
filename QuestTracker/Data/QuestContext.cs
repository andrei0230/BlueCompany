using Dapper;
using MySql.Data.MySqlClient;
using QuestTracker.Models;
using System.Data;

namespace QuestTracker.Data
{
    /// <summary>
    /// Contains methods that interact with the database.
    /// </summary>
    public class QuestContext
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Creates a new QuestContext that connects to the database.
        /// </summary>
        public QuestContext(string? connectionString)
        {
            _db = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Gets all Quests from the database.
        /// </summary>
        public async Task<IEnumerable<Quest>> GetAll()
        {
            return await _db.QueryAsync<Quest>("SELECT * FROM quests");
        }

        /// <summary>
        /// Gets the specified Quest from the database.
        /// </summary>
        public async Task<Quest> GetById(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<Quest>("SELECT * FROM quests WHERE id = @Id", new { Id = id });
        }

        /// <summary>
        /// Creates a new Quest and checks if the owner exists and if he has enough tokens to create it.
        /// If the owner has enough tokens, the value of the Quest will be subtracted from his tokens.
        /// </summary>
        public async Task<int> Create(Quest quest)
        {
            var ownerExists = await _db.ExecuteScalarAsync<bool>("SELECT COUNT(*) FROM users WHERE id = @OwnerId", quest);

            if (!ownerExists)
            {
                return 0;
            }

            int ownerTokens = await _db.QueryFirstOrDefaultAsync<int>("SELECT tokens FROM users WHERE id = @OwnerId", quest);

            if (ownerTokens - quest.Value <= 0)
            {
                return 0;
            }

            ownerTokens -= quest.Value;
            await _db.ExecuteAsync("UPDATE users SET tokens = @Tokens WHERE id = @Id", new { Tokens = ownerTokens, Id = quest.OwnerId });

            return await _db.ExecuteAsync("INSERT INTO quests (ownerId, title, description, value) VALUE (@OwnerId, @Title, @Description, @Value)", quest);
        }

        /// <summary>
        /// Updates a Quests title and description.
        /// </summary>
        public async Task<int> Update(Quest quest)
        {
            return await _db.ExecuteAsync("UPDATE quests SET title = @Title, description = @Description WHERE id = @Id", quest);
        }

        /// <summary>
        /// Deletes the specified Quest.
        /// </summary>
        public async Task<int> Delete(int id)
        {
            return await _db.ExecuteAsync("DELETE FROM quests WHERE id = @Id", new { Id = id });
        }

        /// <summary>
        /// Assigns the specified Quest to the specified user.
        /// </summary>
        public async Task<int> AssignToUser(int id, int userId)
        {
            var userExists = await _db.ExecuteScalarAsync<bool>("SELECT COUNT(*) FROM users WHERE id = @UserId", new { UserId = userId });

            if (!userExists)
            {
                return 0;
            }

            return await _db.ExecuteAsync("UPDATE quests SET userId = @UserId WHERE id = @Id", new { Id = id, UserId = userId });
        }

        /// <summary>
        /// Completes a Quest and adds its value to the assigned users tokens.
        /// </summary>
        public async Task<int> Complete(int id)
        {
            bool isCompleted = await _db.QueryFirstOrDefaultAsync<bool>("SELECT completed FROM quests WHERE id = @Id", new { Id = id });
            bool isAssigned = await _db.ExecuteScalarAsync<bool>("SELECT userId FROM quests WHERE id = @Id", new { Id = id });

            if (isCompleted || !isAssigned)
            {
                return 0;
            }

            int value = await _db.QueryFirstOrDefaultAsync<int>("SELECT value FROM quests WHERE id = @Id", new { Id = id });
            int tokens = await _db.QueryFirstOrDefaultAsync<int>("SELECT u.tokens FROM users u JOIN quests q ON u.id = q.userId WHERE q.id = @Id", new { Id = id });

            tokens += value;

            await _db.ExecuteAsync("UPDATE users SET tokens = @Tokens WHERE id = (SELECT userId FROM quests WHERE id = @Id) ", new { tokens, Id = id });

            return await _db.ExecuteAsync("UPDATE quests SET completed = 1 WHERE id = @Id", new { Id = id });
        }
    }
}
