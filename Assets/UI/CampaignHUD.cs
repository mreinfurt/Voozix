#region Namespaces

using System;
using Entities;
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

        public GameObject LevelSummaryContainer;
        public Text LevelSummaryScoreText;
        public Text LevelSummaryStarText;
        public Text LevelSummayTimeText;

        public Button LevelSummaryContinueButton;
        public Button LevelSummaryTryAgainButton;
        public Button LevelSummaryQuitButton;

        [SerializeField] private PostEffectsController postEffectsController;

        public int TotalStars;

        #region Methods

        public void Start()
        {
            this.LevelSummaryContainer.SetActive(false);
            this.postEffectsController.PostEffect = PostEffect.Shockwave;
            this.ScoreText.text = "0";
            this.TimeText.text = "0";
            this.StarText.text = "0/" + this.TotalStars;

            Events.Player.OnStarCollected += OnStarCollected;
            Events.Player.OnScoreChanged += OnScoreChanged;
            Events.Player.OnReachedGoal += OnReachedGoal;

            this.LevelSummaryContinueButton.onClick.AddListener(HandleOnLevelSummaryContinueButtonClick);
            this.LevelSummaryTryAgainButton.onClick.AddListener(HandleOnLevelSummaryTryAgainButtonClick);
            this.LevelSummaryQuitButton.onClick.AddListener(HandleOnLevelSummaryQuitButtonClick);
        }

        private void HandleOnLevelSummaryContinueButtonClick()
        {
        }

        private void HandleOnLevelSummaryTryAgainButtonClick()
        {
        }

        private void HandleOnLevelSummaryQuitButtonClick()
        {
        }

        private void OnReachedGoal(Vector2 playerPosition)
        {
            this.postEffectsController.PostEffect = PostEffect.Blur;
            var time = new TimeSpan(0, 0, 0, 0, (int)(this.CampaignController.CurrentTime * 1000));

            this.LevelSummaryContainer.SetActive(true);
            this.LevelSummaryScoreText.text = PlayerDataHolder.Instance.Data.Score.ToString();
            this.LevelSummaryStarText.text = "0/" + this.TotalStars;

            this.LevelSummaryContinueButton.Select();
            this.TimeText.text = this.LevelSummayTimeText.text = time.ToString();
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