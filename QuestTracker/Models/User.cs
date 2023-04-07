namespace QuestTracker.Models
{
    public class User
    {
        private int _tokens;

        public int Id { get; set; }

        public string? Name { get; set; }

        public int Tokens { get { return _tokens; } }

        public void AddTokens(int tokens)
        {
            _tokens += tokens;
        }
    }
}
