using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text GameOverLabel;
    public PlayerController Player;

    public Text ScoreLabel;

    private void Start()
    {
        Player.OnScoreChanged += HandleScoreChanged;
        Player.OnPlayerDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        GameOverLabel.text = GameOverLabel.text + "\n" + ScoreLabel.text;
        GameOverLabel.enabled = true;
    }

    private void HandleScoreChanged(int score)
    {
        ScoreLabel.text = "Score: " + score;
    }

    private void Update()
    {
    }
}