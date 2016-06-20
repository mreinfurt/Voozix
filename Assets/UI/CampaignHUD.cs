#region Namespaces

using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class CampaignHUD : MonoBehaviour
    {
        public Text ScoreText;
        public Text TimeText;
        public Text StarText;
        public CampaignController CampaignController;

        public int TotalStars;

        #region Methods

        public void Start()
        {
            this.ScoreText.text = "0";
            this.TimeText.text = "0";
            this.StarText.text = "0/" + this.TotalStars;

            Events.Player.OnStarCollected += OnStarCollected;
            Events.Player.OnScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged(int totalScore, int difference, Vector2 playerPosition)
        {
            this.ScoreText.text = totalScore.ToString();
        }

        private void OnStarCollected()
        {
            this.StarText.text = this.CampaignController.Stars + "/" + this.TotalStars;
        }

        public void Update()
        {
            var time = new TimeSpan(0, 0, 0, 0, (int)(this.CampaignController.CurrentTime * 1000));
            this.TimeText.text = time.ToString();
        }

        #endregion
    }
}