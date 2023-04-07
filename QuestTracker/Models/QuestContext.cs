﻿using Dapper;
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

        public async Task<int> Update(Quest quest)
        {
            return await _db.ExecuteAsync("UPDATE quests SET title = @Title, description = @Description, value = @Value WHERE id = @Id", quest);
        }

        public async Task<int> Delete(int id) 
        {
            return await _db.ExecuteAsync("DELETE FROM quests WHERE id = @Id", new { Id = id });
        }

        public async Task<int> AssignToUser(int  id, int userId)
        {
            var userExists = await _db.ExecuteScalarAsync<bool>("SELECT COUNT(*) FROM users WHERE id = @UserId", new { UserId = userId });

            if (userExists)
            {
                return await _db.ExecuteAsync("UPDATE quests SET userId = @UserId WHERE id = @Id", new { Id = id, UserId = userId });
            }

            return 0;
        }

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

            await _db.ExecuteAsync("UPDATE users SET tokens = @Tokens WHERE id = (SELECT userId FROM quests WHERE id = @Id) ", new { tokens = tokens, Id = id });

            return await _db.ExecuteAsync("UPDATE quests SET completed = 1 WHERE id = @Id", new { Id = id });
        }
    } 
}
