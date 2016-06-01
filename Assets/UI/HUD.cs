#region Namespaces

using Data;
using Events;
using Game;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    /// <summary>
    /// User interface while ingame
    /// </summary>
    public class HUD : MonoBehaviour
    {
        #region Fields

        private float currentGameOverAnimationTime = 0;
        private float currentGameOverScoreAnimationTime = 0;

        // Game Over
        public Text GameOverLabel;
        public Text InfoLabel;

        // Ingame
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
        }

        private void HandlePlayerDeath(PlayerData playerData, Vector2 position)
        {
            this.ScoreValueLabel.text = "<color=\"#fffc19\">" + playerData.Score + "</color>\n<color=\"#fffc19\">" +
                                        playerData.HighestScore + "</color>";
            this.ShowGameOverInformation(true);
        }

        private void HandleScoreChanged(int totalScore, int difference, Vector2 position)
        {
            this.InfoLabel.text = "Score: " + totalScore;

            // Only show score gains
            if (difference <= 0)
            {
                return;
            }

            this.ScoreLabel.text = "" + totalScore;
            this.ScoreLabel.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
            this.ScoreLabel.color = Color.white;

            this.scoreCurrentAnimationState = 0;
            this.showScore = true;
        }

        private void Update()
        {
            this.UpdateScore();
            this.UpdateGameOver();

            var isResetInput = UnityEngine.Input.GetKeyDown(KeyCode.R);

            if (UnityEngine.Input.touchCount > 0)
            {
                isResetInput = isResetInput | UnityEngine.Input.GetTouch(0).phase == TouchPhase.Ended;
            }

            if (isResetInput && GameStateController.GameState != GameState.InGame)
            {
                Global.OnReset();
                this.ShowGameOverInformation(false);
            }
        }

        private void UpdateGameOver()
        {
            this.currentGameOverAnimationTime += Time.deltaTime;
            this.currentGameOverScoreAnimationTime += Time.deltaTime;

            this.currentGameOverAnimationTime = Utility.Tween.LinearScaleInOut(this.GameOverLabel.gameObject,
                new Vector2(0.8f, 1f), this.currentGameOverAnimationTime);

            var gameOverAnimationSpeed = 3f;

            Utility.Tween.LinearScaleOut(this.ScoreHighlightBox.gameObject,
                new Vector2(0, 1.25f), this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
            Utility.Tween.LinearScaleOut(this.ScoreTitleLabel.gameObject,
                new Vector2(0, 1), this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
            this.currentGameOverScoreAnimationTime = Utility.Tween.LinearScaleOut(this.ScoreValueLabel.gameObject,
                new Vector2(0, 1), this.currentGameOverScoreAnimationTime, gameOverAnimationSpeed);
        }

        private void ShowGameOverInformation(bool visible)
        {
            this.currentGameOverScoreAnimationTime = 0;
            this.UpdateGameOver();
            this.GameOverLabel.enabled = visible;
            this.ScoreTitleLabel.enabled = visible;
            this.ScoreValueLabel.enabled = visible;
            this.ScoreHighlightBox.enabled = visible;
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

        #endregion
    }
}