#region Namespaces

using Data;
using Entities;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

#endregion

namespace UI
{
    /// <summary>
    /// User interface while ingame
    /// </summary>
    public class HUD : MonoBehaviour
    {
        #region Fields

        public GameObject ButtonContainer;
        public Button ContinueButton;

        private float currentGameOverScoreAnimationTime = 0;
        public Text InfoLabel;
        public Button QuitButton;

        private float scoreCurrentAnimationState;
        private float scoreCurrentScale;
        public SpriteRenderer ScoreHighlightBox;
        public Text ScoreLabel;
        public Text ScoreTitleLabel;
        public Text ScoreValueLabel;
        private bool showScore;

        #endregion

        #region Methods

        private void Start()
        {
            Player.OnScoreChanged += this.HandleScoreChanged;
            Player.OnDeathEnd += this.HandlePlayerDeath;

            this.ScoreLabel.text = string.Empty;
            this.ContinueButton.onClick.AddListener(HandleOnContinueButtonClick);
            this.QuitButton.onClick.AddListener(HandleOnQuitButtonClick);
        }

        private void HandleOnContinueButtonClick()
        {
            Global.OnReset();
            this.ShowGameOverInformation(false);
        }

        private void HandleOnQuitButtonClick()
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

        private void HandlePlayerDeath(PlayerData playerData, Vector2 position)
        {
            this.ScoreValueLabel.text = "<color=\"#fffc19\">" + playerData.Score + "</color>\n<color=\"#fffc19\">" +
                                        playerData.HighestScore + "</color>";
            this.ShowGameOverInformation(true);
        }

        private void HandleScoreChanged(int totalScore, int difference)
        {
            this.InfoLabel.text = "Score: " + totalScore;

            // Only show score gains
            if (difference <= 0)
            {
                return;
            }

            this.ScoreLabel.text = "" + totalScore;
            this.ScoreLabel.gameObject.transform.position =
                Camera.main.WorldToScreenPoint(PlayerDataHolder.Instance.Player.transform.position);
            this.ScoreLabel.color = Color.white;

            this.scoreCurrentAnimationState = 0;
            this.showScore = true;
        }

        private void Update()
        {
            this.UpdateScore();
            this.UpdateGameOver();
        }

        private void UpdateGameOver()
        {
            this.currentGameOverScoreAnimationTime += Time.deltaTime;

            var gameOverAnimationSpeed = 3f;

            Utility.Tween.LinearScaleOut(this.ScoreHighlightBox.gameObject, new Vector2(0, 1.25f),
                this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
            Utility.Tween.LinearScaleOut(this.ScoreTitleLabel.gameObject, new Vector2(0, 1),
                this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
            this.currentGameOverScoreAnimationTime = Utility.Tween.LinearScaleOut(this.ScoreValueLabel.gameObject,
                new Vector2(0, 1), this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
        }

        private void ShowGameOverInformation(bool visible)
        {
            this.currentGameOverScoreAnimationTime = 0;
            this.UpdateGameOver();
            this.ScoreTitleLabel.enabled = visible;
            this.ScoreValueLabel.enabled = visible;
            this.ScoreHighlightBox.enabled = visible;
            this.ButtonContainer.SetActive(visible);

            if (visible)
            {
                this.ContinueButton.Select();
            }
        }

        private void UpdateScore()
        {
            if (!this.showScore)
            {
                return;
            }

            this.scoreCurrentAnimationState += Time.deltaTime * 5;
            this.scoreCurrentScale = Mathf.Lerp(0.25f, 1f, this.scoreCurrentAnimationState);

            if (this.scoreCurrentAnimationState >= 1f)
            {
                this.ScoreLabel.color = Color.Lerp(Color.white, Color.clear, this.scoreCurrentAnimationState - 1);

                if (this.scoreCurrentAnimationState > 2f)
                {
                    this.showScore = false;
                }
            }

            this.ScoreLabel.gameObject.transform.localScale = new Vector3(this.scoreCurrentScale, this.scoreCurrentScale,
                this.scoreCurrentScale);
        }

        private void OnDestroy()
        {
            Player.OnScoreChanged -= this.HandleScoreChanged;
            Player.OnDeathEnd -= this.HandlePlayerDeath;
        }

        #endregion
    }
}