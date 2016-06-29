#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Data
{
    /// <summary>
    /// Stores all chapter relevant information which has to persist between sessions and levels.
    /// </summary>
    [Serializable]
    public class ChapterData
    {
        #region Fields

        private List<LevelData> levelData = new List<LevelData>();

        #endregion

        #region Properties

        public List<LevelData> LevelData
        {
            get { return this.levelData; }
            set { this.levelData = value; }
        }

        /// <summary>
        /// Returns true if the user has completed all levels of the chapter
        /// </summary>
        public bool Finished
        {
            get { return this.levelData.Count(x => x.Completed) == this.levelData.Count; }
        }

        #endregion
    }
}