namespace QuestTracker.Models
{
    /// <summary>
    /// Quest Model.
    /// </summary>
    public class Quest
    {
        /// <summary>
        /// Unique Id for Quests.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents which user created the Quest.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Represents which user will complete the Quest and recive tokens.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The Quest title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// The Quest description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Represents the amount of tokens the owner has to "pay" to create the Quest and the amount the user recives upon completion.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Represents the state of the Quest.
        /// </summary>
        public bool Completed { get; set; }
    }
}
