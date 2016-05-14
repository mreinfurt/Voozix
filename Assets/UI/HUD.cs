using Entities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        public Text GameOverLabel;
        public Text ScoreLabel;

        private void Start()
        {
            Events.Player.OnScoreChanged += this.HandleScoreChanged;
            Events.Player.OnDeath += this.HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            this.GameOverLabel.text = this.GameOverLabel.text + "\n" + this.ScoreLabel.text;
            this.GameOverLabel.enabled = true;
        }

        private void HandleScoreChanged(int score)
        {
            this.ScoreLabel.text = "Score: " + score;
        }

        private void Update()
        {
        }
    }
}