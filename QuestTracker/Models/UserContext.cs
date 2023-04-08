using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace QuestTracker.Models
{
    /// <summary>
    /// Contains methods that interact with the database. 
    /// </summary>
    public class UserContext
    {
        private readonly IDbConnection _db;

        /// <summary>
        /// Creates a new UserContext that connects to the database.
        /// </summary>
        public UserContext(string? connectionString)
        {
            _db = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Gets all users from the database.
        /// </summary>
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.QueryAsync<User>("SELECT * FROM users"); 
        }

        /// <summary>
        /// Gets the specified user from the databse.
        /// </summary>
        public async Task<User> GetById(int id)
        {
            return await _db.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE id = @Id", new { Id = id });
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int> Create(User user)
        {
            return await _db.ExecuteAsync("INSERT INTO users (name) VALUES (@Name)", user);
        }

        /// <summary>
        /// Updates the specified users name.
        /// </summary>
        public async Task<int> Update(User user)
        {
            return await _db.ExecuteAsync("UPDATE users SET name = @Name WHERE id = @Id", user);
        }

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        public async Task<int> Delete(int id)
        {
            return await _db.ExecuteAsync("DELETE FROM users WHERE id = @Id", new { Id = id });
        }
    }
}
