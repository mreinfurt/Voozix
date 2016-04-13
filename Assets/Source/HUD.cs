using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public PlayerController Player;

    public Text ScoreLabel;

    public Text GameOverLabel;

	// Use this for initialization
	void Start () {
        this.Player.OnScoreChanged += this.HandleScoreChanged;
        this.Player.OnPlayerDeath += this.HandlePlayerDeath;
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

    // Update is called once per frame
	void Update () {
	}
}
