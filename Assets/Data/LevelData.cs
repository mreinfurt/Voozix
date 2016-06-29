#region Namespaces

using System;

#endregion

namespace Data
{
    /// <summary>
    /// Stores all level relevant information which has to persist between sessions and levels.
    /// </summary>
    [Serializable]
    public class LevelData
    {
        #region Fields

        private bool completed;

        private float completionTime;

        private int score;

        private int stars;

        #endregion

        #region Properties

        public int Score
        {
            get { return this.score; }
            set { this.score = value; }
        }

        public int Stars
        {
            get { return this.stars; }
            set { this.stars = value; }
        }

        public bool Completed
        {
            get { return this.completed; }
            set { this.completed = value; }
        }

        public float CompletionTime
        {
            get { return this.completionTime; }
            set { this.completionTime = value; }
        }

        #endregion
    }
}