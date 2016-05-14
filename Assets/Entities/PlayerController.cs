using System;
using UnityEngine;

namespace Entities
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 movement;

        private int score;

        public float Speed = 7.5f;

        public int Score
        {
            get { return this.score; }
            private set
            {
                this.score = value;
                Events.Player.OnScoreChanged(this.score);
            }
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.HandleInput();
            this.HandleMovement();
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.movement.x = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.movement.x = -1;
            }
            else
            {
                this.movement.x = 0;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.movement.y = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.movement.y = -1;
            }
            else
            {
                this.movement.y = 0;
            }
        }

        private void HandleMovement()
        {
            var position = new Vector3(this.Speed * this.movement.x * Time.deltaTime, this.Speed * this.movement.y * Time.deltaTime, 0);
            this.transform.position += position;
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            switch (collider.gameObject.tag.ToLower())
            {
                case "star":
                    this.Score += 50;
                    Events.Player.OnStarCollected();
                    break;
                case "enemy":
                    Events.Player.OnDeath();
                    break;
            }
        }

        public void Reset()
        {
            this.Score = 0;
        }
    }
}