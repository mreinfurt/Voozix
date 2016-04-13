using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;

    public Action OnPlayerDeath;

    public Action OnStarCollected;

    public Action<int> OnScoreChanged;

    private int score;

    public int Score
    {
        get { return this.score; }
        private set
        {
            this.score = value;
            OnScoreChanged(this.score);
        }
    }

    public float Speed = 7.5f;

    private void Start()
    {
    }

    private void Update()
    {
        HandleMovement();
        HandleWallBounds();
    }

    private void HandleMovement()
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

        var position = new Vector3(Speed * movement.x * Time.deltaTime, Speed * movement.y * Time.deltaTime, 0);
        this.transform.position += position;
    }

    /// Keeps the player inside the screen space
    private void HandleWallBounds()
    {
        var newPosition = transform.position;
        var screenBounds = Utility.GetScreenSpaceBounds();

        if (newPosition.x < screenBounds.x || newPosition.x > screenBounds.x + screenBounds.width)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, screenBounds.x, screenBounds.x + screenBounds.width);
        }

        if (newPosition.y < screenBounds.y || newPosition.y > screenBounds.y + screenBounds.height)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, screenBounds.y, screenBounds.y + screenBounds.height);
        }

        transform.position = newPosition;
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