#region Namespaces

using UnityEngine;
using Screen = Utility.Screen;

#endregion

namespace Entities
{
    public class StarController : MonoBehaviour
    {
        #region Fields

        private float currentAnimationState;

        public GameObject EnemyPrefab;

        private bool growing = true;

        public GameObject StarCollectionParticleSystem;

        #endregion

        #region Properties

        public float Scale
        {
            get { return this.transform.localScale.x; }
        }

        #endregion

        #region Methods

        private void Start()
        {
        }

        private void Update()
        {
            this.currentAnimationState += Time.deltaTime;
            this.currentAnimationState = Utility.Tween.LinearScaleInOut(this.gameObject, new Vector2(0.5f, 1f), this.currentAnimationState);
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
                    this.transform.position =
                        new Vector3(Random.Range(screenBounds.x, screenBounds.x + screenBounds.width),
                            Random.Range(screenBounds.y, screenBounds.y + screenBounds.height), 1);
                } while (Vector2.Distance(this.transform.position, playerPosition) < distanceToPlayer);
            }
        }

        #endregion
    }
}