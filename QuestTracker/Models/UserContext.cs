using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace QuestTracker.Models
{
    public class UserContext
    {
        private readonly IDbConnection _db;

        public UserContext(string? connectionString)
        {
            _db = new MySqlConnection(connectionString);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.QueryAsync<User>("SELECT * FROM users"); 
        }

        public async Task<int> Create(User user)
        {
            return await _db.ExecuteAsync("INSERT INTO users (name) VALUES (@Name)", user);
        }
    }
}
