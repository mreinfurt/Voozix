#region Namespaces

using System.Collections.Generic;
using Data;
using Entities;
using UnityEngine;

#endregion

namespace Game
{
    public class CampaignController : MonoBehaviour
    {
        #region Fields

        public int Chapter = 1;

        private float currentTime = 0;
        public int Level = 1;

        #endregion

        #region Methods

        private void Start()
        {
            Events.Player.OnReachedGoal += ReachedGoal;
        }

        private void ReachedGoal(Vector2 playerPosition)
        {
            var data = PlayerDataHolder.Instance.Data;

            if (data.ChapterData == null)
            {
                data.ChapterData = new List<ChapterData>();
            }

            if (data.ChapterData.Count < this.Chapter)
            {
                data.ChapterData.Add(new ChapterData()
                {
                    LevelData = {new LevelData()}
                });
            }

            var levelData = PlayerDataHolder.Instance.Data.ChapterData[this.Chapter - 1].LevelData[this.Level - 1];
            levelData.Score = data.Score;
            levelData.Completed = true;
            levelData.CompletionTime = this.currentTime;

            PlayerDataSaveController.Save(data);
        }

        private void Update()
        {
            this.currentTime += Time.deltaTime;
        }

        #endregion
    }
}