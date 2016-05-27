#region Namespaces

using UnityEngine;
using Screen = Utility.Screen;

#endregion

namespace Entities
{
    public class EnemyController : MonoBehaviour
    {
        #region Fields

        public Vector2 Movement;

        public float Speed = 3.5f;

        #endregion

        #region Methods

        private void Start()
        {
            this.GetComponent<ClampToScreenBounds>().OnHitScreenBounds += this.ReverseDirection;
        }

        public void Spawn(Vector2 playerPosition)
        {
            var screenBounds = Screen.GetScreenSpaceBounds();

            // Avoid spwaning near the player
            var distanceToPlayer = 3.5f;

            do
            {
                this.transform.position = new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
                    Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);
            } while (Vector2.Distance(this.transform.position, playerPosition) < distanceToPlayer);

            var horizontal = Random.Range(0, 10);

            if (horizontal <= 5)
            {
                this.Movement.x = 1;
            }
            else
            {
                this.Movement.y = 1;
            }

            this.transform.localScale = Vector3.zero;
        }

        /// <summary>
        /// Reverse directions when hitting screen bounds
        /// </summary>
        /// <param name="direction"></param>
        private void ReverseDirection()
        {
            this.Movement *= -1;
        }

        private void Update()
        {
            this.HandleMovement();

            if (this.transform.localScale.x < 1)
            {
                var warpSpeed = 0.025f;
                var newScale = this.transform.localScale;
                newScale.x += warpSpeed;
                newScale.y += warpSpeed;
                newScale.z += warpSpeed;
                this.transform.localScale = newScale;
            }
            else
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        private void HandleMovement()
        {
            var position = new Vector3(this.Speed * this.Movement.x * Time.deltaTime,
                this.Speed * this.Movement.y * Time.deltaTime);
            this.transform.position += position;
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            var colliderTag = collider.gameObject.tag.ToLower();
            if (colliderTag == "star" || colliderTag == "enemy")
            {
                return;
            }

            this.ReverseDirection();
            this.HandleMovement();
        }

        #endregion
    }
}