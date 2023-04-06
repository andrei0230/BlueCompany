namespace QuestTracker.Models
{
    public class User
    {
        #region Fields

        private int _tokens;

        #endregion

        #region Properties

        public int Id { get; set; }

        public string? Name { get; set; }

        public int Tokens { get { return _tokens; } }

        #endregion

        #region Methods

        public void AddTokens(int tokens)
        {
            _tokens += tokens;
        }

        #endregion
    }
}
