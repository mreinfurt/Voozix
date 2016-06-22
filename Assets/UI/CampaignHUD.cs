#region Namespaces

using System;
using Entities;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

#endregion

namespace UI
{
    public class CampaignHUD : MonoBehaviour
    {
        #region Fields

        public CampaignController CampaignController;

        public GameObject LevelSummaryContainer;

        public Button LevelSummaryContinueButton;
        public Button LevelSummaryQuitButton;
        public Text LevelSummaryScoreText;
        public Text LevelSummaryStarText;
        public Button LevelSummaryTryAgainButton;
        public Text LevelSummayTimeText;

        [SerializeField] private PostEffectsController postEffectsController;
        public Text ScoreText;
        public Text StarText;
        public Text TimeText;

        public int TotalStars;

        #endregion

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
            const string sceneToLoad = "C1_L2";
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
        }

        private void HandleOnLevelSummaryTryAgainButtonClick()
        {
            const string sceneToLoad = "C1_L1";
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
        }

        private void HandleOnLevelSummaryQuitButtonClick()
        {
            const string sceneToLoad = "MainMenu";
            if (NiceSceneTransition.instance != null)
            {
                NiceSceneTransition.instance.LoadScene(sceneToLoad);
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
            }
        }

        private void OnReachedGoal(Vector2 playerPosition)
        {
            this.postEffectsController.PostEffect = PostEffect.Blur;
            var time = new TimeSpan(0, 0, 0, 0, (int) (this.CampaignController.CurrentTime * 1000));

            this.LevelSummaryContainer.SetActive(true);
            this.LevelSummaryScoreText.text = PlayerDataHolder.Instance.Data.Score.ToString();
            this.LevelSummaryStarText.text = this.CampaignController.Stars + "/" + this.TotalStars;

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
            var time = new TimeSpan(0, 0, 0, 0, (int) (this.CampaignController.CurrentTime * 1000));
            this.TimeText.text = string.Format("{0:00}:{1:00}:{2:00}",
                (int) time.TotalHours,
                time.Minutes,
                time.Seconds);  
        }

        #endregion
    }
}