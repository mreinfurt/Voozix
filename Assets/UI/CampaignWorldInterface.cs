#region Namespaces

using Entities;
using Game;
using UnityEngine;
using UnityEngine.UI;

#endregion

namespace UI
{
    public class CampaignWorldInterface : MonoBehaviour
    {
        #region Fields

        private float scoreCurrentAnimationState;
        private float scoreCurrentScale;
        public Text ScoreLabel;
        private bool showScore;

        #endregion

        #region Methods

        private void Start()
        {
            Events.Player.OnScoreChanged += this.HandleScoreChanged;
            this.ScoreLabel.text = string.Empty;
        }

        private void HandleScoreChanged(int totalScore, int difference)
        {
            // Only show score gains
            if (difference <= 0)
            {
                return;
            }

            this.ScoreLabel.text = "" + totalScore;
            this.ScoreLabel.gameObject.transform.position = PlayerDataHolder.Instance.Player.transform.position;
            this.ScoreLabel.color = Color.white;

            this.scoreCurrentAnimationState = 0;
            this.showScore = true;
        }

        private void Update()
        {
            this.UpdateScore();

            var isResetInput = UnityEngine.Input.GetKeyDown(KeyCode.R);

            if (UnityEngine.Input.touchCount > 0)
            {
                isResetInput = isResetInput | UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;
            }

            if (isResetInput && GameStateController.GameState != GameState.InGame)
            {
                Events.Global.OnReset();
                // TODO: Show level overview
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
            Events.Player.OnScoreChanged -= this.HandleScoreChanged;
        }

        #endregion
    }
}