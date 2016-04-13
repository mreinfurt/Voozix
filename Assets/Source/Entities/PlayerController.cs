using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;

    public Action OnPlayerDeath;

    public Action<int> OnScoreChanged;

    public Action OnStarCollected;

    private int score;

    public float Speed = 7.5f;

    public int Score
    {
        get { return this.score; }
        private set
        {
            this.score = value;
            OnScoreChanged(this.score);
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1;
        }
        else
        {
            movement.x = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.y = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.y = -1;
        }
        else
        {
            movement.y = 0;
        }
    }

    private void HandleMovement()
    {
        var position = new Vector3(Speed * movement.x * Time.deltaTime, Speed * movement.y * Time.deltaTime, 0);
        this.transform.position += position;
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        switch (collider.gameObject.tag.ToLower())
        {
            case "star":
                this.Score += 50;
                this.OnStarCollected();
                break;
            case "enemy":
                this.OnPlayerDeath();
                break;
        }
    }

    public void Reset()
    {
        this.Score = 0;
    }
}