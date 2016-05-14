﻿using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public Text GameOverLabel;

        private float scoreCurrentAnimationState;
        private float scoreCurrentScale;
        public Text ScoreLabel;
        private bool showScore;

        private void Start()
        {
            Player.OnScoreChanged += this.HandleScoreChanged;
            Player.OnDeath += this.HandlePlayerDeath;

            this.ScoreLabel.text = string.Empty;
        }

        private void HandlePlayerDeath()
        {
            this.GameOverLabel.text = this.GameOverLabel.text;
            this.GameOverLabel.enabled = true;
        }

        private void HandleScoreChanged(int totalScore, int difference, Vector2 position)
        {
            this.ScoreLabel.text = "+" + difference;
            this.ScoreLabel.gameObject.transform.position = Camera.main.WorldToScreenPoint(position);
            this.ScoreLabel.color = Color.white;

            this.scoreCurrentAnimationState = 0;
            this.showScore = true;
        }

        private void Update()
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
    }
}