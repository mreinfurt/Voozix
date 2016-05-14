using Events;
using UnityEngine;

namespace Entities
{
    public class PlayerController : MonoBehaviour
    {
        public bool IsAlive = true;
        private Vector2 movement;

        private int score;

        public float Speed = 5.5f;

        public int Score
        {
            get { return this.score; }
            private set
            {
                var difference = value - this.score;
                this.score = value;

                Player.OnScoreChanged(this.score, difference, this.transform.position);
            }
        }

        private void Start()
        {
            Global.OnReset += OnReset;
        }

        private void OnReset()
        {
            this.Score = 0;
            this.IsAlive = true;
        }

        private void Update()
        {
            if (!this.IsAlive)
            {
                return;
            }

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
            var position = new Vector3(this.Speed * this.movement.x * Time.deltaTime,
                this.Speed * this.movement.y * Time.deltaTime, 0);
            this.transform.position += position;
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            switch (collider.gameObject.tag.ToLower())
            {
                case "star":
                    this.Score += 20;
                    Player.OnStarCollected();
                    break;
                case "enemy":
                    Player.OnDeath();
                    this.IsAlive = false;
                    break;
            }
        }
    }
}