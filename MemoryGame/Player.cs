namespace MemoryGame
{
    public class Player
    {
        private string name;
        private int score;
        private bool turn;

        public Player(string name, int score, bool turn)
        {
            this.name = name;
            this.score = score;
            this.turn = turn;
        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string newName)
        {
            name = newName;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetScore(int newScore)
        {
            score = newScore;
        }

        public bool GetTurn()
        {
            return turn;
        }

        public void SetTurn(bool newTurn)
        {
            turn = newTurn;
        }
    }
}
