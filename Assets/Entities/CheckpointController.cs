#region Namespaces

using UnityEngine;

#endregion

namespace Entities
{
    public class CheckpointController : MonoBehaviour
    {
        #region Fields

        private AudioSource audioSource;

        #endregion

        #region Methods

        private void Start()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            Events.Player.OnReachedCheckpoint += OnReachedCheckpoint;
        }

        private void OnReachedCheckpoint(Vector2 vector2)
        {
            this.audioSource.Play(0);
        }

        private void Update()
        {
        }

        #endregion
    }
}