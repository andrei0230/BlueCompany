namespace QuestTracker.Models
{
    /// <summary>
    /// User Model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique Id for users.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Users name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Represents the amount of tokens the user has.
        /// </summary>
        public int Tokens { get; set; }
    }
}
