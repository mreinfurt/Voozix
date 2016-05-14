using UnityEngine;

namespace Entities
{
    public class StarController : MonoBehaviour
    {
        private float currentAnimationState;

        private float currentScale = 1f;

        public GameObject EnemyPrefab;

        private bool growing = true;

        public GameObject StarCollectionParticleSystem;

        private void Start()
        {
        }

        private void Update()
        {
            this.currentAnimationState += Time.deltaTime;
            this.currentScale = this.growing
                ? Mathf.Lerp(0.75f, 1.35f, this.currentAnimationState)
                : Mathf.Lerp(1.35f, 0.75f, this.currentAnimationState);

            if (this.currentAnimationState >= 1f)
            {
                this.currentAnimationState = 0;
                this.growing = !this.growing;
            }

            this.transform.localScale = new Vector3(this.currentScale, this.currentScale, this.currentScale);
        }

        private void OnCollisionEnter2D(Collision2D collider)
        {
            if (collider.gameObject.tag.ToLower() == "player")
            {
                this.StarCollectionParticleSystem.transform.position = this.transform.position;
                this.StarCollectionParticleSystem.GetComponent<ParticleSystem>().Stop();
                this.StarCollectionParticleSystem.GetComponent<ParticleSystem>().Play();

                var screenBounds = Utility.Screen.GetScreenSpaceBounds();
                this.transform.position = new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
                    Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);
            }
        }
    }
}