#region Namespaces

using UnityEngine;

#endregion

namespace Entities
{
    public class CheckpointController : MonoBehaviour
    {
        #region Fields

        private AudioSource audioSource;

        private bool hasBeenActivated = false;

        private Material material;

        #endregion

        #region Methods

        private void Start()
        {
            this.material = this.GetComponent<SpriteRenderer>().material;
            this.audioSource = this.GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.gameObject.tag.ToLower() == "player")
            {
                this.ReachedCheckpoint();
            }
        }

        private void ReachedCheckpoint()
        {
            if (this.hasBeenActivated)
            {
                return;
            }

            this.material.SetFloat("_Activated", 1);
            this.audioSource.Play(0);
            this.hasBeenActivated = true;
        }

        private void Update()
        {
        }

        #endregion
    }
}