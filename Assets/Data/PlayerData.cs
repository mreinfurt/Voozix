using System;

namespace Data
{
    /// <summary>
    /// Stores all player relevant information which has to persist between sessions and levels.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public int HighestScore;

        private int score;

        public int Score
        {
            get { return this.score; }
            set
            {
                this.score = value;
                if (this.score > this.HighestScore)
                {
                    this.HighestScore = this.score;
                }
            }
        }
    }
}