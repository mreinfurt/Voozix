#region Namespaces

using UnityEngine;

#endregion

namespace Entities
{
    public class MusicSoundController : MonoBehaviour
    {
        #region Fields

        private AudioSource audioSource;

        public float Volume = 1;

        #endregion

        #region Methods

        private void Start()
        {
            this.audioSource = this.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (this.audioSource == null)
            {
                return;
            }

            this.audioSource.volume = PlayerDataHolder.Instance.Data.MusicEnabled ? this.Volume : 0;
        }

        #endregion
    }
}