using UnityEngine;
using Screen = Utility.Screen;

namespace Entities
{
    public class EnemyController : MonoBehaviour
    {
        public Vector2 Movement;

        public float Speed = 5f;

        private void Start()
        {
            this.GetComponent<ClampToScreenBounds>().OnHitScreenBounds += this.OnHitScreenBounds;
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
        }

        /// <summary>
        /// Reverse directions when hitting screen bounds
        /// </summary>
        /// <param name="direction"></param>
        private void OnHitScreenBounds(Vector2 direction)
        {
            if (direction.x >= 1)
            {
                this.Movement.x *= -1;
            }
            else
            {
                this.Movement.y *= -1;
            }
        }

        private void Update()
        {
            this.HandleMovement();
        }

        private void HandleMovement()
        {
            var position = new Vector3(this.Speed * this.Movement.x * Time.deltaTime,
                this.Speed * this.Movement.y * Time.deltaTime);
            this.transform.position += position;
        }
    }
}