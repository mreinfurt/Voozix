using UnityEngine;
using Screen = Utility.Screen;

namespace Entities
{
    public class StarController : MonoBehaviour
    {
        private float currentAnimationState;

        private float currentScale = 1f;

        public GameObject EnemyPrefab;

        private bool growing = true;

        public GameObject StarCollectionParticleSystem;

        public float Scale
        {
            get { return this.currentScale; }
        }

        private void Start()
        {
        }

        private void Update()
        {
            this.currentAnimationState += Time.deltaTime;
            this.currentScale = this.growing
                ? Mathf.Lerp(0.5f, 1f, this.currentAnimationState)
                : Mathf.Lerp(1f, 0.5f, this.currentAnimationState);

            if (this.currentAnimationState >= 1f)
            {
                this.currentAnimationState = 0;
                this.growing = !this.growing;
            }

            this.transform.localScale = new Vector3(this.currentScale, this.currentScale, this.currentScale);
        }

        private void OnCollisionEnter2D(Collision2D collisionInformation)
        {
            if (collisionInformation.gameObject.tag.ToLower() == "player")
            {
                this.StarCollectionParticleSystem.transform.position = this.transform.position;
                //this.StarCollectionParticleSystem.GetComponent<ParticleSystem>().Stop();
                //this.StarCollectionParticleSystem.GetComponent<ParticleSystem>().Play();
                this.GetComponent<AudioSource>().Play();

                var playerPosition = collisionInformation.gameObject.transform.position;
                var screenBounds = Screen.GetScreenSpaceBounds();
                var distanceToPlayer = 1.5f;

                do
                {
                    this.transform.position = new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
                        Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);
                } while (Vector2.Distance(this.transform.position, playerPosition) < distanceToPlayer);
            }
        }
    }
}