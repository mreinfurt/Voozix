using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public int HighestScore;

        private int score;
        public int Score
        {
            get { return this.score; }
            set {
                this.score = value;
                if (this.score > this.HighestScore)
                {
                    this.HighestScore = this.score;
                }
            }
        }
    }
}