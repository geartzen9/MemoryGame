namespace MemoryGame
{
    public class Player
    {
        // The name of the player.
        private string name;

        // The score of the player.
        private int score;

        // The turn of the player.
        private bool turn;

        /// <summary>
        ///     Initialize a new player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="score">The score of the player.</param>
        /// <param name="turn">The turn of the player.</param>
        public Player(string name, int score, bool turn)
        {
            this.name = name;
            this.score = score;
            this.turn = turn;
        }

        /// <summary>
        ///     Get the name of the player.
        /// </summary>
        /// <returns>The name of the player.</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        ///     Set the name of the player.
        /// </summary>
        /// <param name="newName">The new name for the player.</param>
        public void SetName(string newName)
        {
            name = newName;
        }

        /// <summary>
        ///     Get the score of the player.
        /// </summary>
        /// <returns>The score of the player.</returns>
        public int GetScore()
        {
            return score;
        }

        /// <summary>
        ///     Set the score of the player.
        /// </summary>
        /// <param name="newScore">The new score for the player.</param>
        public void SetScore(int newScore)
        {
            score = newScore;
        }

        /// <summary>
        ///     Get the turn of the player.
        /// </summary>
        /// <returns>The turn of the player.</returns>
        public bool GetTurn()
        {
            return turn;
        }

        /// <summary>
        ///     Set the turn of the player.
        /// </summary>
        /// <param name="newTurn">The new turn for the player.</param>
        public void SetTurn(bool newTurn)
        {
            turn = newTurn;
        }
    }
}
