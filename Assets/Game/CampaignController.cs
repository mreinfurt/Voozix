#region Namespaces

using System;
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
        private int starsCollected = 0;
        public int Level = 1;

        #endregion

        #region Properties

        public float CurrentTime
        {
            get { return this.currentTime; }
        }

        public int Stars
        {
            get { return this.starsCollected; }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Events.Player.OnReachedGoal += ReachedGoal;
            Events.Player.OnStarCollected += OnStarCollected;
        }

        private void OnStarCollected()
        {
            this.starsCollected += 1;
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
            levelData.Stars = this.starsCollected;

            PlayerDataSaveController.Save(data);
        }

        private void Update()
        {
            this.currentTime += Time.deltaTime;
        }

        #endregion
    }
}