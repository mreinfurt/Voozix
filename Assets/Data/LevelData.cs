﻿#region Namespaces

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

        #endregion

        #region Properties

        public int Score
        {
            get { return this.score; }
        }

        public bool Completed
        {
            get { return this.completed; }
        }

        public float CompletionTime
        {
            get { return this.completionTime; }
        }

        #endregion
    }
}