#region Namespaces

using System;

#endregion

namespace Data
{
    /// <summary>
    /// Stores all player relevant information which has to persist between sessions and levels.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        #region Fields

        public bool MusicEnabled = true;

        public int HighestScore;

        private int score;

        #endregion

        #region Properties

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

        #endregion
    }
}