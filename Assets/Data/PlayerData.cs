#region Namespaces

using System;
using System.Collections.Generic;

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

        private List<ChapterData> chapterData = new List<ChapterData>();

        public int HighestScore;

        public bool MusicEnabled = true;

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

        public List<ChapterData> ChapterData
        {
            get { return this.chapterData; }
        }

        #endregion
    }
}