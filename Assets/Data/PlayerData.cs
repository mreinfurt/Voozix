using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class PlayerData
    {
        public List<int> Scores = new List<int>();

        public int CurrentScore
        {
            get { return this.Scores.LastOrDefault(); }
            set
            {
                if (this.Scores.Count <= 0)
                {
                    this.Scores.Add(0);
                }
                    
                this.Scores[Scores.Count - 1] = value;
            }
        }
    }
}